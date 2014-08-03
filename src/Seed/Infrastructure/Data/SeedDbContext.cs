using System;
using System.Data.Entity;
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
            modelBuilder.Entity<User>()
                .Property(MappingHelpers.GetMember<User, User.UserState>("State"))
                .HasColumnName("State")
                .IsRequired();
        
            base.OnModelCreating(modelBuilder);
        }
    }
}
