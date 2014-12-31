using System;
using Seed.Common.Domain;

namespace Seed.Security
{
    internal static class AccountLockoutPolicy
    {
        public static int MaxFailedLoginAtteptCount = 5;
        public static TimeSpan FailedLoginAttemptWindowPeriod = TimeSpan.FromMinutes(5);

        public static bool AutomaticallyUnlockAccount = true;
        public static TimeSpan AutomaticallyUnlockAccountAfter = TimeSpan.FromMinutes(10);

        public static bool IsAccountLocked(User user)
        {
            if (!user.LockedUtcDate.HasValue)
            {
                return false;
            }

            if (AutomaticallyUnlockAccount)
            {
                var lockExpiry = user.LockedUtcDate.Value.Add(AutomaticallyUnlockAccountAfter);

                if (ClockProvider.GetUtcNow() > lockExpiry)
                {
                    user.Unlock();

                    return false;
                }
            }

            return true;
        }

        public static void LogSuccessfulLoginAttempt(User user)
        {
            user.FailedLoginWindowStart = null;
            user.FailedLoginAttemptCount = 0;
        }

        public static void LogFailedLoginAttempt(User user)
        {
            if (!user.FailedLoginWindowStart.HasValue)
            {
                user.FailedLoginWindowStart = ClockProvider.GetUtcNow();
            }

            var failedLoginWindowExpiry = user.FailedLoginWindowStart.Value.Add(FailedLoginAttemptWindowPeriod);

            if (ClockProvider.GetUtcNow() <= failedLoginWindowExpiry)
            {
                user.FailedLoginAttemptCount++;
            }
            else
            {
                user.FailedLoginAttemptCount = 1;
            }

            // Update the FailedLoginWindowStart date again to create a rolling window.
            user.FailedLoginWindowStart = ClockProvider.GetUtcNow();

            if (user.FailedLoginAttemptCount == MaxFailedLoginAtteptCount)
            {
                user.Lock();
            }
        }
    }
}
