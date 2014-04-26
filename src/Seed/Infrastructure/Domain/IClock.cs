using System;

namespace Seed.Infrastructure.Domain
{
    public interface IClock
    {
        DateTime GetUtcNow();
    }
}
