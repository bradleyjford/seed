using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Seed.Common.Data
{
    public static class QueryablePagingExtensions
    {
        private static readonly MethodInfo PagedMethod = typeof(QueryablePagingExtensions).GetMethods()
            .Single(method => 
                method.Name == "Paged" && 
                method.GetParameters().Length == 3 &&
                method.GetParameters()[2].ParameterType == typeof(SortDescriptor[]));

        public static IQueryable<T> Paged<T>(this IQueryable<T> source, IPagingOptions options, SortDescriptor defaultSort)
        {
            return Paged(source, options, new[] { defaultSort });
        }

        public static IQueryable<T> Paged<T>(this IQueryable<T> source, IPagingOptions options, SortDescriptor[] defaultSort)
        {
            var firstResult = (options.PageNumber - 1) * options.PageSize;

            var sortDescriptors = options.SortDescriptors;

            if (!sortDescriptors.Any())
            {
                sortDescriptors = defaultSort;
            }

            return source
                .OrderBy(sortDescriptors)
                .Skip(firstResult)
                .Take(options.PageSize);
        }

        public static IQueryable Paged(this IQueryable source, Type type, IPagingOptions options, SortDescriptor defaultSort)
        {
            return Paged(source, type, options, new[] { defaultSort });
        }

        public static IQueryable Paged(this IQueryable source, Type type, IPagingOptions options, SortDescriptor[] defaultSort)
        {
            return (IQueryable)PagedMethod.MakeGenericMethod(type).Invoke(null, new object[] { source, options, defaultSort });
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
