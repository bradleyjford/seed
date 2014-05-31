﻿using System;

namespace Seed.Common.Domain
{
    public interface IInlineAudited
    {
        int CreatedByUserId { get; }
        DateTime CreatedUtcDate { get; }
        int ModifiedByUserId { get; }
        DateTime ModifiedUtcDate { get; }
    }
}