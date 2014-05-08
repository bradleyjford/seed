using System;

namespace Seed.Api.Security
{
    public class IdentityResponse
    {
        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public string[] Roles { get; private set; }

        public IdentityResponse(string userName, string fullName, string[] roles)
        {
            UserName = userName;
            FullName = fullName;
            Roles = roles;
        }
    }
}