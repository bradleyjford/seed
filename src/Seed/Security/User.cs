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

        public User(string username, string fullName, string emailAddress)
        {
            Username = username;
            FullName = fullName;
            EmailAddress = emailAddress;

            IsActive = true;
        }

        [StringLength(100)]
        public string Username { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(150)]
        public string EmailAddress { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; protected set; }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}