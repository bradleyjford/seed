using System;
using System.Collections.Generic;

namespace Seed.Common.Data
{
    public class PagingOptions : IPagingOptions
    {
        private const int DefaultPageSize = 50;

        private readonly List<SortDescriptor> _sortOrder = new List<SortDescriptor>(); 

        private int _pageNumber = 1;
        private int _pageSize = DefaultPageSize;

        public List<SortDescriptor> SortOrder
        {
            get { return _sortOrder; }
        }

        public int ItemCount { get; set; }

        public int PageNumber
        {
            get { return _pageNumber; } 

            set
            {
                _pageNumber = value;

                if (_pageNumber <= 0)
                {
                    _pageNumber = 1;
                }
            }
        }
        
        public int PageSize
        {
            get { return _pageSize; }

            set 
            { 
                _pageSize = value;
            
                if (_pageSize <= 0)
                {
                    _pageSize = DefaultPageSize;
                }
            }
        }

        public int PageCount 
        { 
            get { return (int)Math.Ceiling(((double)ItemCount / PageSize)); }
        }
    }
}