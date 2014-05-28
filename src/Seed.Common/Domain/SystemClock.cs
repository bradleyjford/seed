using System;

namespace Seed.Common.Domain
{
    public class SystemClock : IClock
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
