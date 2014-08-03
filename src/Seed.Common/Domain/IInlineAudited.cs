using System;

namespace Seed.Common.Domain
{
    public interface IInlineAudited<out TUserId>
    {
        TUserId CreatedByUserId { get; }
        DateTime CreatedUtcDate { get; }
        TUserId ModifiedByUserId { get; }
        DateTime ModifiedUtcDate { get; }
    }
}