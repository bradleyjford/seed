using System;

namespace Seed.Infrastructure.Domain
{
    public interface IInlineAudited
    {
        int CreatedByUserId { get; }
        DateTime CreatedUtcDate { get; }
        int ModifiedByUserId { get; }
        DateTime ModifiedUtcDate { get; }
    }
}