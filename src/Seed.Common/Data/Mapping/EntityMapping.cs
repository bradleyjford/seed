using System;
using System.Data.Entity.ModelConfiguration;
using Seed.Common.Domain;

namespace Seed.Common.Data.Mapping
{
    public abstract class EntityMapping<TEntity, TId> : EntityTypeConfiguration<TEntity>
        where TEntity : Entity<TId> 
        where TId : struct
    {
        protected EntityMapping(string tableName, string schemaName = "dbo")
        {
            ToTable(tableName, schemaName);

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnOrder(1);
        }
    }
}
