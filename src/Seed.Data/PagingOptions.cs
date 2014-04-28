using System;

namespace Seed.Data
{
    public class PagingOptions : IPagingOptions
    {
        private const int DefaultPageSize = 50;

        private int _pageNumber = 1;
        private int _pageSize = DefaultPageSize;

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
            get { return (int)Math.Ceiling((double)(ItemCount / PageSize)); }
        }

    }
}