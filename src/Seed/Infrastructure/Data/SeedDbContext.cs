using System;
using System.Data.Entity;
using Seed.Common.Data;
using Seed.Infrastructure.Auditing;
using Seed.Lookups;
using Seed.Security;

namespace Seed.Infrastructure.Data
{
    public class SeedDbContext : DbContext, ISeedDbContext
    {
        public SeedDbContext()
            : base("Seed")
        {
        }

        public DbSet<AuditEvent> AuditEvents { get; set; }
        public DbSet<User> Users { get; set; }        
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(SeedDbContext).Assembly);
        
            base.OnModelCreating(modelBuilder);
        }
    }
}
