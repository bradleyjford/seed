using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Common.Domain
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        private readonly object _hashCodeLock = new object();
        private int? _hashCode;

        public TId Id { get; protected internal set; }

        // TODO: Refactor data annotations into EF convention
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TId>;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            
            return Equals(other);
        }

        public bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            
            if (other.GetType() != GetType()) return false;

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            if (!_hashCode.HasValue)
            {
                lock (_hashCodeLock)
                {
                    if (!_hashCode.HasValue)
                    {
                        _hashCode = EntityHashCodeCalculator.CalculateHashCode(this);
                    }
                }
            }

            return _hashCode.Value;
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }
    }
}
