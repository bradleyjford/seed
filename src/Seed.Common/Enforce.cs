using System;

namespace Seed.Common
{
    public static class Enforce
    {
        public static void ArgumentNotNull<T>(string parameter, T value)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameter);
            }
        }
    }
}
