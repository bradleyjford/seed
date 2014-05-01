using System;

namespace Seed.Infrastructure.Domain
{
    public abstract class AggregateRoot<TId> : IInlineAudited
    {
        public void Initialize(IUserContext userContext, IClock clock)
        {
            CreatedUtcDate = ModifiedUtcDate = clock.GetUtcNow();
            CreatedByUserId = ModifiedByUserId = userContext.UserId;
        }

        public TId Id { get; protected internal set; }

        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedUtcDate { get; protected set; }
        public int ModifiedByUserId { get; protected set; }
        public DateTime ModifiedUtcDate { get; protected set; }
    }
}
