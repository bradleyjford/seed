using System;
using Seed.Common.Domain;

namespace Seed.Common.Tests
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
