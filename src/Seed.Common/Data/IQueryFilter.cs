using System;
using System.Linq;

namespace Seed.Common.Data
{
    public interface IQueryFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> queryable);
    }
}