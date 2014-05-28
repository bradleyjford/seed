using System;

namespace Seed.Common.Domain
{
    public interface IClock
    {
        DateTime GetUtcNow();
    }
}
