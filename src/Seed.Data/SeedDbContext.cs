using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Seed.Infrastructure.Auditing;
using Seed.Lookups;
using Seed.Security;

namespace Seed.Data
{
    public interface ISeedDbContext
    {
        DbSet<AuditEvent> AuditEvents { get; }
        DbSet<User> Users { get; }
        DbSet<Country> Countries { get; }

        DbChangeTracker ChangeTracker { get; }

        DbSet<T> Set<T>()
            where T : class;

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }

    public class SeedDbContext : DbContext, ISeedDbContext
    {
        public SeedDbContext()
            : base("Seed")
        {
        }

        public DbSet<AuditEvent> AuditEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
