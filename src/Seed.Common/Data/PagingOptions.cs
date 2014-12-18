using System;
using System.Collections.Generic;

namespace Seed.Common.Data
{
    public interface IPagingOptions
    {
        int PageNumber { get; }
        int PageSize { get; }

        IEnumerable<SortDescriptor> SortDescriptors { get; }
    }

    public class PagingOptions : IPagingOptions
    {
        private const int DefaultPageSize = 50;

        private int _pageNumber = 1;
        private int _pageSize = DefaultPageSize;

        public string SortOrder { get; set; }

        public IEnumerable<SortDescriptor> SortDescriptors
        {
            get { return SortDescriptorParser.Parse(SortOrder); }
        }

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
    }
}