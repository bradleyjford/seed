using System;

namespace Seed.Security
{
    public enum LoginResult
    {
        InvalidUserNameOrPassword,
        Success,
        TwoFactorAuthenticationRequired,
        PendingConfirmation,
        AccountLocked
    }

    public static class LoginResultExtensions
    {
        public static bool IsSuccessful(this LoginResult result)
        {
            switch (result)
            {
                case LoginResult.Success:
                case LoginResult.TwoFactorAuthenticationRequired:
                    return true;

                default:
                    return false;
            }
        }
    }
}
