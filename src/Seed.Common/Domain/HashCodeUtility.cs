﻿using System;
using System.Collections;

namespace Seed.Common.Domain
{
    // Adapted from Josh Bloch's book "Effective Java" - http://www.amazon.com/dp/0321356683/.
    public static class HashCodeUtility
    {
        private const int OddPrimeNumber = 37;

        public static readonly int Seed = 23;

        private static int GetFirstTerm(int seed)
        {
            return OddPrimeNumber * seed;
        }

        public static int Hash(int seed, bool value)
        {
            return GetFirstTerm(seed) + (value ? 1 : 0);
        }

        public static int Hash(int seed, char value)
        {
            return GetFirstTerm(seed) + value;
        }

        public static int Hash(int seed, int value)
        {
            return GetFirstTerm(seed) + value;
        }

        public static int Hash(int seed, long value)
        {
            return GetFirstTerm(seed) + (int)(value ^ (value >> 32));
        }

        public static int Hash(int seed, float value)
        {
            return Hash(seed, (int)BitConverter.DoubleToInt64Bits(value));
        }

        public static int Hash(int seed, double value)
        {
            return Hash(seed, BitConverter.DoubleToInt64Bits(value));
        }

        public static int Hash(int seed, object obj)
        {
            var result = seed;

            var items = obj as IEnumerable;

            if (obj == null)
            {
                result = Hash(result, 0);
            }
            else if (items != null)
            {
                foreach (var item in items)
                {
                    result = Hash(result, item);
                }
            }
            else
            {
                result = Hash(result, obj.GetHashCode());
            }

            return result;
        }
    }
}
