using System;
using System.Collections.Generic;
using Seed.Common;
using Seed.Common.Domain;
using Seed.Common.Security;

namespace Seed.Security
{
    public partial class User : AggregateRoot<Guid, Guid>
    {
        private readonly UserStateMachine _stateMachine;

        protected User()
        {
            State = UserState.New;
            UserClaims = new List<UserClaim>();

            _stateMachine = new UserStateMachine(() => State, s => State = s);

            _stateMachine.Configure(UserState.Confirmed)
                .OnEntry(() => IsConfirmed = true);

            _stateMachine.Configure(UserState.Activated)
                .OnEntry(() => IsActive = true)
                .OnExit(() => IsActive = false);

            _stateMachine.Configure(UserState.Locked)
                .OnEntry(() => LockedUtcDate = ClockProvider.GetUtcNow())
                .OnExit(() => LockedUtcDate = null);

            _stateMachine.Configure(UserState.PasswordChangeRequired)
                .OnEntry(() => IsPasswordChangeRequired = true)
                .OnExit(() => IsPasswordChangeRequired = false);
        }

        public User(
            string userName, 
            string fullName, 
            string email, 
            IPasswordHasher passwordHasher, 
            string password)
            : this()
        {
            UserName = Enforce.ArgumentNotNull("userName", userName);
            FullName = Enforce.ArgumentNotNull("fullName", fullName);
            Email = Enforce.ArgumentNotNull("emailAddress", email);
            Enforce.ArgumentNotNull("password", password);

            Id = GuidCombIdGenerator.GenerateId();

            HashedPassword = passwordHasher.ComputeHash(password);

            CreatedUtcDate = ModifiedUtcDate = LastPasswordChangeUtcDate = ClockProvider.GetUtcNow();
        }

        private UserState State { get; set; }

        public string UserName { get; private set; }

        public string HashedPassword { get; private set; }

        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email address associated with the user.
        /// </summary>
        public string Email { get; set; }

        private ICollection<UserClaim> UserClaims { get; set; }

        /// <summary>
        /// Gets the security claims attributed to the user.
        /// </summary>
        public IEnumerable<UserClaim> Claims
        {
            get { return UserClaims; }
        }

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
        /// being able to login to the application.
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
        /// <param name="newPassword">The new password to be set</param>
        public void ChangePassword(IPasswordHasher hasher, string newPassword)
        {
            Enforce.ArgumentNotNull("hasher", hasher);
            Enforce.ArgumentNotNull("newPassword", hasher);

            _stateMachine.Fire(Trigger.PasswordChanged);

            HashedPassword = hasher.ComputeHash(newPassword);

            LastPasswordChangeUtcDate = ClockProvider.GetUtcNow();
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

        public void AddClaim(UserClaim claim)
        {
            Enforce.ArgumentNotNull("claim", claim);

            UserClaims.Add(claim);
        }

        public void RemoveClaim(UserClaim claim)
        {
            Enforce.ArgumentNotNull("claim", claim);

            UserClaims.Remove(claim);
        }

        internal void Lock()
        {
            _stateMachine.Fire(Trigger.Lock);

            LockedUtcDate = ClockProvider.GetUtcNow();
        }

        public void Unlock()
        {
            _stateMachine.Fire(Trigger.UnlockAccount);

            LockedUtcDate = null;
        }
    }
}
