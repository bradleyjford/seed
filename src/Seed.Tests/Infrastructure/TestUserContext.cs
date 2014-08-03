using System;
using System.Collections.Generic;
using System.Linq;
using Seed.Security;

namespace Seed.Tests.Infrastructure
{
    public class TestUserContext : IUserContext<Guid>
    {
        private readonly IEnumerable<string> _roles;

        public TestUserContext(
            Guid userId,
            string displayName,
            string userName,
            string email,
            IEnumerable<string> roles)
        {
            _roles = roles;
            UserId = userId;
            DisplayName = displayName;
            UserName = userName;
            Email = email;
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
