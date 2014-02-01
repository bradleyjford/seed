using System;

namespace Seed.Api.Security
{
    public class SignInSuccessResult : SignInResult
    {
        public string Message
        {
            get { return "Success."; }
        }
    }
}