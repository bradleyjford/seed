using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Threading.Tasks;

namespace Seed.Common.Data
{
    public static class QueryablePagingExtensions
    {
        private static readonly MethodInfo PagedMethod = typeof(QueryablePagingExtensions).GetMethods()
            .Single(method =>
                method.Name == "ToPagedResultAsync" && 
                method.GetParameters()[2].ParameterType == typeof(SortDescriptor[]));

        public static Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> source, 
            IPagingOptions options, 
            SortDescriptor defaultSort) 
            where T : class
        {
            return ToPagedResultAsync(source, options, new[] { defaultSort });
        }

        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> source, 
            IPagingOptions options, 
            SortDescriptor[] defaultSort) 
            where T : class
        {
            var firstResult = (options.PageNumber - 1) * options.PageSize;

            var sortDescriptors = options.SortDescriptors;

            if (!sortDescriptors.Any())
            {
                sortDescriptors = defaultSort;
            }

            var itemCount = await source.CountAsync();

            var items = await source
                .OrderBy(sortDescriptors)
                .Skip(firstResult)
                .Take(options.PageSize)
                .ToListAsync();

            return new PagedResult<T>(options.PageNumber, options.PageSize, items, itemCount);
        }

        public static dynamic ToPagedResultAsync(
            this IQueryable source, 
            Type type, 
            IPagingOptions options, 
            SortDescriptor defaultSort)
        {
            return ToPagedResultAsync(source, type, options, new[] { defaultSort });
        }

        public static dynamic ToPagedResultAsync(
            this IQueryable source, 
            Type type, 
            IPagingOptions options, 
            SortDescriptor[] defaultSort)
        {
            var result = PagedMethod.MakeGenericMethod(type)
                .Invoke(null, new object[] { source, options, defaultSort });

            return result;
        }

        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> source, 
            IEnumerable<SortDescriptor> sortDescriptors)
        {
            if (sortDescriptors == null) throw new ArgumentNullException("sortDescriptors");
            if (!sortDescriptors.Any()) throw new ArgumentException("No SortDescriptors specified", "sortDescriptors");

            IOrderedQueryable<T> result = null;

            foreach (var sortDescriptor in sortDescriptors)
            {
                if (sortDescriptor.Direction == SortDirection.Ascending)
                {
                    result = result == null ?
                        source.OrderByProperty(sortDescriptor.PropertyName) :
                        result.ThenByProperty(sortDescriptor.PropertyName);
                }
                else
                {
                    result = result == null ?
                        source.OrderByPropertyDescending(sortDescriptor.PropertyName) :
                        result.ThenByPropertyDescending(sortDescriptor.PropertyName);
                }
            }

            return result;
        }
    }
}
