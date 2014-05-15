using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Infrastructure.Domain
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected internal set; }

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }
    }
}