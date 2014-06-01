using System;
using System.Collections.Generic;

namespace Seed.Common.Data
{
    public interface IPagingOptions
    {
        int PageNumber { get; }
        int PageSize { get; }

        string SortOrder { get; set; }

        IEnumerable<SortDescriptor> SortDescriptors { get; }
    }
}