﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Seed.Common;
using Seed.Common.Domain;
using Seed.Infrastructure.Data;
using Stateless;

using IPasswordHasher = Seed.Common.Security.IPasswordHasher;

namespace Seed.Security
{
    public class User : AggregateRoot<Guid, Guid>, IUser<Guid>
    {
        internal enum UserState
        {
            New,
            Confirmed,
            Activated,
            Deactivated,
            Locked,
            PasswordChangeRequired
        }

        private enum Trigger
        {
            ConfirmEmail,
            Activate,
            Deactivate,
            Lock,
            UnlockAccount,
            RequireChangePassword,
            PasswordChanged
        }

        private readonly StateMachine<UserState, Trigger> _stateMachine;

        protected User()
        {
            State = UserState.New;

            _stateMachine = new StateMachine<UserState, Trigger>(() => State, s => State = s);

            _stateMachine.Configure(UserState.New)
                .Permit(Trigger.ConfirmEmail, UserState.Confirmed);

            _stateMachine.Configure(UserState.Confirmed)
                .OnEntry(() => IsConfirmed = true)
                .Permit(Trigger.Activate, UserState.Activated);

            _stateMachine.Configure(UserState.Activated)
                .OnEntry(() => IsActive = true)
                .OnExit(() => IsActive = false)
                .SubstateOf(UserState.Confirmed)
                .Permit(Trigger.Deactivate, UserState.Deactivated)
                .Permit(Trigger.Lock, UserState.Locked)
                .Permit(Trigger.RequireChangePassword, UserState.PasswordChangeRequired);

            _stateMachine.Configure(UserState.Deactivated)
                .SubstateOf(UserState.Confirmed)
                .Permit(Trigger.Activate, UserState.Activated)
                .Ignore(Trigger.Deactivate);

            _stateMachine.Configure(UserState.Locked)
                .OnEntry(() => LockedUtcDate = ClockProvider.GetUtcNow())
                .OnExit(() => LockedUtcDate = null)
                .SubstateOf(UserState.Activated)
                .SubstateOf(UserState.Deactivated)
                .Permit(Trigger.UnlockAccount, UserState.Activated)
                .Ignore(Trigger.PasswordChanged)
                .Ignore(Trigger.Lock);

            _stateMachine.Configure(UserState.PasswordChangeRequired)
                .OnEntry(() => IsPasswordChangeRequired = true)
                .OnExit(() => IsPasswordChangeRequired = false)
                .SubstateOf(UserState.Confirmed);
        }

        public User(
            string userName, 
            string fullName, 
            string emailAddress, 
            IPasswordHasher passwordHasher, 
            string password)
            : this()
        {
            Enforce.ArgumentNotNull("userName", userName);
            Enforce.ArgumentNotNull("fullName", fullName);
            Enforce.ArgumentNotNull("emailAddress", emailAddress);
            Enforce.ArgumentNotNull("hashedPassword", password);

            Id = GuidCombIdGenerator.GenerateId();

            UserName = userName;
            FullName = fullName;
            EmailAddress = emailAddress;
            HashedPassword = passwordHasher.ComputeHash(password);

            LoginProviders = new List<LoginProvider>();

            CreatedUtcDate = ModifiedUtcDate = LastPasswordChangeUtcDate = ClockProvider.GetUtcNow();
        }

        private UserState State { get; set; }

        string IUser<Guid>.UserName
        {
            get { return UserName; }
            set { UserName = value; }
        }

        [StringLength(100)]
        public string UserName { get; private set; }

        [StringLength(150)]
        public string HashedPassword { get; private set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(150)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets any notes that may have been entered by an administrator of the application.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets the date and time of the last successful login by the user in UTC.
        /// </summary>
        public DateTime? LastLoginUtcDate { get; private set; }

        /// <summary>
        /// Gets the date and time at which the user's password was last changed in UTC.
        /// </summary>
        public DateTime LastPasswordChangeUtcDate { get; private set; }

        public DateTime? FailedLoginWindowStart { get; internal set; }

        public int FailedLoginAttemptCount { get; internal set; }

        public DateTime? LockedUtcDate { get; private set; }

        public List<LoginProvider> LoginProviders { get; private set; }

        public void Activate()
        {
            _stateMachine.Fire(Trigger.Activate);
        }

        public void Deactivate()
        {
            _stateMachine.Fire(Trigger.Deactivate);
        }

        public void Confirm()
        {
            _stateMachine.Fire(Trigger.ConfirmEmail);
            _stateMachine.Fire(Trigger.Activate);
        }

        /// <summary>
        /// Gets a value indicating if the user has been de-activated by an administrator of
        /// the application.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Gets a value indicating if the user has confirmed their email address.
        /// </summary>
        public bool IsConfirmed { get; private set; }

        /// <summary>
        /// Gets a value indicating if the user is required to change their password before
        /// being able to signin to the application.
        /// </summary>
        public bool IsPasswordChangeRequired { get; private set; }

        /// <summary>
        /// Gets a value indicating if the user is locked.
        /// </summary>
        public bool IsLocked
        {
            get { return AccountLockoutPolicy.IsAccountLocked(this); }
        }

        /// <summary>
        /// Changes the password used by the user to signin to the application using the specfied password hasher.
        /// </summary>
        /// <param name="hasher"><see cref="Seed.Common.Security.IPasswordHasher"/></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(IPasswordHasher hasher, string newPassword)
        {
            Enforce.ArgumentNotNull("hasher", hasher);
            Enforce.ArgumentNotNull("newPassword", hasher);

            HashedPassword = hasher.ComputeHash(newPassword);

            LastPasswordChangeUtcDate = ClockProvider.GetUtcNow();

            _stateMachine.Fire(Trigger.PasswordChanged);
        }

        public LoginResult Login(IPasswordHasher passwordHasher, string password)
        {
            Enforce.ArgumentNotNull("passwordHasher", passwordHasher);
            Enforce.ArgumentNotNull("password", password);

            var passwordValidated = passwordHasher.ValidateHash(HashedPassword, password);

            if (passwordValidated)
            {
                if (!IsConfirmed)
                {
                    return LoginResult.PendingConfirmation;
                }

                if (AccountLockoutPolicy.IsAccountLocked(this))
                {
                    return LoginResult.AccountLocked;
                }

                LastLoginUtcDate = ClockProvider.GetUtcNow();

                AccountLockoutPolicy.LogSuccessfulLoginAttempt(this);

                return LoginResult.Success;
            }

            AccountLockoutPolicy.LogFailedLoginAttempt(this);
            
            return LoginResult.InvalidUserNameOrPassword;
        }

        public void AddLoginProvider(string name, string userKey)
        {
            Enforce.ArgumentNotNull("name", name);
            Enforce.ArgumentNotNull("userKey", userKey);

            LoginProviders.Add(new LoginProvider(name, userKey));
        }

        public void RemoveLoginProvider(string name, string userKey)
        {
            Enforce.ArgumentNotNull("name", name);
            Enforce.ArgumentNotNull("userKey", userKey);

            var loginProvider = 
                LoginProviders.FirstOrDefault(lp => lp.Name == name && lp.UserKey == userKey);

            if (loginProvider != null)
            {
                LoginProviders.Remove(loginProvider);
            }
        }

        internal void Lock()
        {
            _stateMachine.Fire(Trigger.Lock);
        }

        public void Unlock()
        {
            _stateMachine.Fire(Trigger.UnlockAccount);
        }
    }
}
