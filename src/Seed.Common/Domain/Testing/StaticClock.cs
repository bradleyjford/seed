using System;

namespace Seed.Common.Domain.Testing
{
    public class StaticClock : IClock
    {
        private readonly DateTime _utcDateTime;

        public StaticClock(DateTime utcDateTime)
        {
            _utcDateTime = utcDateTime;
        }

        public DateTime GetUtcNow()
        {
            return _utcDateTime;
        }
    }
}
