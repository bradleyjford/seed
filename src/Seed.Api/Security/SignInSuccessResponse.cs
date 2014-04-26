using System;

namespace Seed.Api.Security
{
    public class SignInSuccessResponse : SignInResponse
    {
        public string Username { get; set; }
        public string FullName { get; private set; }
        public string[] Roles { get; private set; }

        public SignInSuccessResponse(string username, string fullName, string[] roles)
        {
            Username = username;
            FullName = fullName;
            Roles = roles;
        }
    }
}