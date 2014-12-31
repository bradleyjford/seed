using System;

namespace Seed.Security
{
    public enum AuthorizationTokenValidationResult
    {
        Success,
        Expired,
        PreviouslyConsumed,
        InvalidSecret,
        NoSuchToken
    }

    public static class AuthorizationTokenValidationResultExtensionMethods
    {
        public static string GetErrorMessage(this AuthorizationTokenValidationResult result)
        {
            switch (result)
            {
                case AuthorizationTokenValidationResult.NoSuchToken:
                    return "Unable to located the specified token";
                
                case AuthorizationTokenValidationResult.Expired:
                    return "The specified token has expired";

                case AuthorizationTokenValidationResult.PreviouslyConsumed:
                    return "The specified token has already been used";

                case AuthorizationTokenValidationResult.InvalidSecret:
                    return "Unable to validate token";

                default:
                    return "Success";
            }
        }
    }
}
