using System;

namespace Seed.Common.Domain
{
    internal static class EntityHashCodeCalculator
    {
        private static readonly Random RandomGenerator = new Random((int)DateTime.UtcNow.Ticks);

        public static int CalculateHashCode<TId>(Entity<TId> entity)
        {
            var result = HashCodeUtility.Hash(HashCodeUtility.Seed, entity.GetType());

            if (default(TId).Equals(entity.Id))
            {
                var random = RandomGenerator.Next(Int32.MinValue, Int32.MaxValue);

                result = HashCodeUtility.Hash(result, random);
            }
            else
            {
                result = HashCodeUtility.Hash(result, entity.Id);
            }

            return result;
        }

    }
}
