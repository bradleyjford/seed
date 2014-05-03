using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Infrastructure.Domain
{
    public abstract class AggregateRoot<TId> : IInlineAudited
    {
        public TId Id { get; protected internal set; }

        public int CreatedByUserId { get; protected set; }
        public DateTime CreatedUtcDate { get; protected set; }
        public int ModifiedByUserId { get; protected set; }
        public DateTime ModifiedUtcDate { get; protected set; }

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }
    }
}
