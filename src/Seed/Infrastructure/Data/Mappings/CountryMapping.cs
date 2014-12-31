using System;
using Seed.Lookups;

namespace Seed.Infrastructure.Data.Mappings
{
    internal class CountryMapping : LookupEntityMapping<Country>
    {
        public CountryMapping() 
            : base("Country")
        {
        }
    }
}
