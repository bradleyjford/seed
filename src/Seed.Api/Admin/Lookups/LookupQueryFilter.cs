using System;
using System.Linq;
using Seed.Common.Data;
using Seed.Common.Domain;

namespace Seed.Api.Admin.Lookups
{
    public class LookupQueryFilter : IQueryFilter<ILookupEntity>
    {
        public string FilterText { get; set; }

        public IQueryable<ILookupEntity> Apply(IQueryable<ILookupEntity> source)
        {
            if (!String.IsNullOrEmpty(FilterText))
            {
                source = source.Where(l => l.Name.StartsWith(FilterText));
            }

            return source;
        }
    }
}