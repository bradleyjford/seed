using System;
using System.Linq;
using Seed.Security;

namespace Seed.Api.Infrastructure.Security
{
    public class SeedUserContext : IUserContext<Guid>
    {
        private readonly string[] _roles;

        public SeedUserContext()
        {
            UserId = new Guid("00000000-0000-0000-0000-000000000001");
            DisplayName = "Administrator 1";
            UserName = "test";
            Email = "admin1@bjf.io";

            _roles = new[] { "admin" };
        }

        public Guid UserId { get; private set; }
        public string DisplayName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }

        public bool IsInRole(string role)
        {
            return _roles.Contains(role);
        }
    }
}