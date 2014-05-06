using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Seed.Data
{
    public static class QueryableExtensions
    {
        public static Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> target, IPagingOptions options)
        {
            var firstResult = (options.PageNumber - 1) * options.PageSize;
           
            return target.Skip(firstResult).Take(options.PageSize).ToListAsync();
        }
        
    }
}