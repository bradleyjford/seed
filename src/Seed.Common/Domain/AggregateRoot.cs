using System;

namespace Seed.Common.Domain
{
    public abstract class AggregateRoot<TId> : Entity<TId>,  IInlineAudited
    {
        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedUtcDate { get; protected set; }
        public int ModifiedByUserId { get; protected set; }
        public DateTime ModifiedUtcDate { get; protected set; }
    }
}
