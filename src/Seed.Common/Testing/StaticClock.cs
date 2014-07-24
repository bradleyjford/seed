using System;
using Seed.Common.Domain;

namespace Seed.Common.Testing
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
