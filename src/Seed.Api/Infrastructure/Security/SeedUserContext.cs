using System;
using System.Linq;
using Seed.Infrastructure.Domain;

namespace Seed.Api.Infrastructure.Security
{
    public class SeedUserContext : IUserContext
    {
        private readonly string[] _roles;

        public SeedUserContext()
        {
            UserId = 1;
            DisplayName = "Administrator 1";
            UserName = "test";
            EmailAddress = "admin1@bjf.io";

            _roles = new[] { "admin" };

        }

        public int UserId { get; private set; }
        public string DisplayName { get; private set; }
        public string UserName { get; private set; }
        public string EmailAddress { get; private set; }

        public bool IsInRole(string role)
        {
            return _roles.Contains(role);
        }
    }
}