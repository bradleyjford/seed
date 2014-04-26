using System;
using Seed.Infrastructure.Domain;

namespace Seed.Security
{
    public class User : AggregateRoot<int>
    {
        public User()
        {
            IsActive = true;
        }

        public string Username { get; set; }

        public string FullName { get; set; }
        public string EmailAddress { get; set; }

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