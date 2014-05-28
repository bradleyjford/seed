using System;

namespace Seed.Data
{
    public interface IPagingOptions
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int ItemCount { get; set; }
        int PageCount { get; }
    }
}