using System;
using System.Collections.Generic;

namespace Seed.Common.Data
{
    public interface IPagingOptions
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int ItemCount { get; set; }
        int PageCount { get; }

        List<SortDescriptor> SortOrder { get; }
    }
}