using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Seed.Infrastructure.Auditing;
using Seed.Lookups;
using Seed.Security;

namespace Seed.Infrastructure.Data
{
    public interface ISeedDbContext
    {
        DbSet<AuditEvent> AuditEvents { get; }
        DbSet<User> Users { get; }
        DbSet<AuthorizationToken> AuthorizationTokens { get; }
        DbSet<Country> Countries { get; }

        DbChangeTracker ChangeTracker { get; }

        DbSet Set(Type entityType);

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
