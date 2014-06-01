using System;
using System.Collections.Generic;

namespace Seed.Common.Data
{
    public class PagedResult<T>
    {
        public PagedResult(int pageNumber, int pageSize, List<T> items, int itemCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
            ItemCount = itemCount;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; private set; }
        public int ItemCount { get; private set; }

        public int PageCount
        {
            get { return (int)Math.Ceiling(((double)ItemCount / PageSize)); }
        }
    }

    public class PagedResult : PagedResult<object>
    {
        public PagedResult(int pageNumber, int pageSize, List<object> items, int itemCount) 
            : base(pageNumber, pageSize, items, itemCount)
        {
        }
    }
}
