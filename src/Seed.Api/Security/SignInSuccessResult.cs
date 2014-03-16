using System;

namespace Seed.Api.Security
{
    public class SignInSuccessResponse : SignInResponse
    {
        public string Message
        {
            get { return "Success."; }
        }
    }
}