using System;
using Seed.Common.Domain;

namespace Seed.Common.Data.Mapping
{
    public abstract class AggregateRootEntityMapping<TAggregateRoot, TId, TUserId> : EntityMapping<TAggregateRoot, TId> 
        where TAggregateRoot : AggregateRoot<TId, TUserId> 
        where TUserId : struct 
        where TId : struct
    {
        protected AggregateRootEntityMapping(string tableName, string schemaName = "dbo") 
            : base(tableName, schemaName)
        {
            Property(e => e.RowVersion)
                .IsRowVersion();
        }
    }
}
