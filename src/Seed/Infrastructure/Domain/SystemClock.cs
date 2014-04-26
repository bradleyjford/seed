using System;

namespace Seed.Infrastructure.Domain
{
    public class SystemClock : IClock
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
