using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Seed.Api.Security
{
    public class IdentityResponse
    {
        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public IEnumerable<string> Roles { get; private set; }

        public IdentityResponse(string userName, string fullName, IEnumerable<Claim> claims)
        {
            UserName = userName;
            FullName = fullName;
            Roles = claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
        }
    }
}