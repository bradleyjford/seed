using System;
using System.Threading.Tasks;

namespace Seed
{
    public static class TaskHelpers
    {
        public static Task ForVoidResult()
        {
            return Task.FromResult<object>(null);
        }
    }
}
