using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Common.Domain
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        public TId Id { get; protected internal set; }

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
            var result = HashCodeUtility.Seed;

            result = HashCodeUtility.Hash(result, GetType());
            result = HashCodeUtility.Hash(result, Id);

            return result;
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