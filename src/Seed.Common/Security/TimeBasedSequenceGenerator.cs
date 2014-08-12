using System;

namespace Seed.Common.Security
{
    public class TimeBasedSequenceGenerator
    {
        private const long UnixEpochTicks = 621355968000000000;
        private const long TicksPerSecond = 10000000;

        private readonly int _windowSeconds;

        public TimeBasedSequenceGenerator(int windowSeconds)
        {
            _windowSeconds = windowSeconds;
        }

        public long GetSequence(DateTime utcDateTime)
        {
            var secondsSinceUnixEpoch = GetSecondsSinceUnixEpoch(utcDateTime);

            return (long)Math.Floor((double)secondsSinceUnixEpoch / (double)_windowSeconds);
        }

        private static long GetSecondsSinceUnixEpoch(DateTime dateTime)
        {
            return (dateTime.Ticks - UnixEpochTicks) / TicksPerSecond;
        }
    }
}
