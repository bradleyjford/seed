using System;

namespace Seed.Api.Security
{
    public class SignInSuccessResponse : SignInResponse
    {
        public string UserName { get; private set; }
        public string[] Roles { get; private set; }

        public SignInSuccessResponse(string userName, string[] roles)
        {
            UserName = userName;
            Roles = roles;
        }
    }
}