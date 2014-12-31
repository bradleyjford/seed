using System;
using Seed.Common.Data.Mapping;
using Seed.Lookups;

namespace Seed.Infrastructure.Data.Mappings
{
    public abstract class LookupEntityMapping<TLookupEntity> : AggregateRootEntityMapping<TLookupEntity, int, Guid> 
        where TLookupEntity : LookupEntity 
    {
        protected LookupEntityMapping(string tableName, string schemaName = "dbo") 
            : base(tableName, schemaName)
        {
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
