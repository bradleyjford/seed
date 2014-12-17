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

    public class PagingOptionsInputModel : IPagingOptions
    {
        private const int DefaultPageSize = 50;

        private int _pageNumber = 1;
        private int _pageSize = DefaultPageSize;

        public string SortOrder { get; set; }

        public IEnumerable<SortDescriptor> SortDescriptors
        {
            get { return ParseSortOrder(SortOrder); }
        }

        private static IEnumerable<SortDescriptor> ParseSortOrder(string sortOrder)
        {
            var result = new List<SortDescriptor>();

            if (String.IsNullOrEmpty(sortOrder))
            {
                return result;
            }

            var sortSpecifications = sortOrder.Split(',');

            foreach (var sortSpecification in sortSpecifications)
            {
                var parts = sortSpecification.Split(' ');

                if (parts.Length == 1)
                {
                    result.Add(new SortDescriptor(parts[0], SortDirection.Ascending));
                }
                else if (parts.Length == 2)
                {
                    var direction = SortDirection.Ascending;

                    if (String.Compare("desc", parts[1], StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        direction = SortDirection.Descending;
                    }

                    result.Add(new SortDescriptor(parts[0], direction));
                }
            }

            return result;
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