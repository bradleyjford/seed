using System;

namespace Seed.Common
{
    public static class Enforce
    {
        public static T ArgumentNotNull<T>(string parameter, T value)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameter);
            }

            return value;
        }
        
        public static string ArgumentNotNullOrEmpty(string parameter, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameter);
            }

            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Argument empty", parameter);
            }

            return value;
        }
    }
}
