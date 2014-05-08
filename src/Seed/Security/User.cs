using System;
using System.ComponentModel.DataAnnotations;
using Seed.Infrastructure.Domain;

namespace Seed.Security
{
    public class User : AggregateRoot<int>
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

            CreatedUtcDate = ModifiedUtcDate = LastPasswordChangeUtcDate = ClockProvider.GetUtcNow();
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
    }
}