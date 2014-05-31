using System;
using System.Linq;

namespace Seed.Common.Data
{
    public static class QueryableFilterExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source, IQueryFilter<T> filter)
        {
            return filter.Apply(source);
        }
    }
}
