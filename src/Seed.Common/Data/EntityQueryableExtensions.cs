using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Seed.Common.Data
{
    public static class EntityQueryableExtensions
    {
        public static Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> target, IPagingOptions options)
        {
            var firstResult = (options.PageNumber - 1) * options.PageSize;

            target = target.OrderBy(options.SortOrder);

            return target.Skip(firstResult).Take(options.PageSize).ToListAsync();
        }

        public static Task<List<object>> ToPagedListAsync(this IQueryable target, IPagingOptions options)
        {
            var firstResult = (options.PageNumber - 1) * options.PageSize;

            dynamic dynamicQuery = target;

            dynamicQuery = dynamicQuery.OrderBy(options.SortOrder);

            return Queryable.Take(Queryable.Skip(dynamicQuery, firstResult), options.PageSize).ToListAsync();
        }

        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> target, 
            IEnumerable<SortDescriptor> sortDescriptors)
        {
            IOrderedQueryable<T> result = null;

            foreach (var sortDescriptor in sortDescriptors)
            {
                if (sortDescriptor.Direction == SortDirection.Ascending)
                {
                    result = result == null ?
                        target.OrderByProperty(sortDescriptor.PropertyName) :
                        result.ThenByProperty(sortDescriptor.PropertyName);
                }
                else
                {
                    result = result == null ?
                        target.OrderByPropertyDescending(sortDescriptor.PropertyName) :
                        result.ThenByPropertyDescending(sortDescriptor.PropertyName);
                }
            }

            return result;
        }
    }
}
