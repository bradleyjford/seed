using System;
using System.Threading.Tasks;

namespace Seed.Common
{
    public static class TaskHelpers
    {
        [Obsolete]
        public static Task ForVoidResult()
        {
            return Task.FromResult<object>(null);
        }
    }
}
