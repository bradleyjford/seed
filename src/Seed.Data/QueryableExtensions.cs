using System;
using System.Linq;

namespace Seed.Data
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> target, IPagingOptions options)
        {
            var firstResult = (options.PageNumber - 1) * options.PageSize;
           
            return target.Skip(firstResult).Take(options.PageSize);
        }
    }
}