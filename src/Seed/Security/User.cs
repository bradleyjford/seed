using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Seed.Common.Domain;

namespace Seed.Security
{
    public class User : AggregateRoot<int>, IUser<int>
    {
        protected User()
        {
        }

        public User(string userName, string fullName, string emailAddress, string hashedPassword)
        {
            UserName = userName;
            FullName = fullName;
            EmailAddress = emailAddress;
            HashedPassword = hashedPassword;

            IsActive = true;
            IsConfirmed = false;

            LoginProviders = new List<LoginProvider>();

            CreatedUtcDate = ModifiedUtcDate = LastPasswordChangeUtcDate = ClockProvider.GetUtcNow();
        }

        string IUser<int>.UserName
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

        public string Notes { get; set; }

        public bool IsActive { get; private set; }

        public bool IsConfirmed { get; private set; }

        public DateTime LastPasswordChangeUtcDate { get; private set; }
        
        public List<LoginProvider> LoginProviders { get; private set; }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void ChangePassword(string newHashedPassword)
        {
            HashedPassword = newHashedPassword;

            LastPasswordChangeUtcDate = ClockProvider.GetUtcNow();
        }

        public void AddLoginProvider(string name, string userKey)
        {
            LoginProviders.Add(new LoginProvider(name, userKey));
        }

        public void RemoveLoginProvider(string name, string userKey)
        {
            var loginProvider = 
                LoginProviders.FirstOrDefault(lp => lp.Name == name && lp.UserKey == userKey);

            if (loginProvider != null)
            {
                LoginProviders.Remove(loginProvider);
            }
        }
    }
}