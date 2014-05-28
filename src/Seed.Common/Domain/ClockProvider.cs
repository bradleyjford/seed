using System;

namespace Seed.Common.Domain
{
    public static class ClockProvider
    {
        private static IClock _clock = new SystemClock();

        public static void SetClock(IClock clock)
        {
            _clock = clock;
        }

        public static DateTime GetUtcNow()
        {
            return _clock.GetUtcNow();
        }
    }
}
