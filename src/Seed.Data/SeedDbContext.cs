using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Seed.Infrastructure.Auditing;
using Seed.Infrastructure.Domain;
using Seed.Lookups;
using Seed.Security;

namespace Seed.Data
{
    public class SeedDbContext : DbContext, ISeedDbContext
    {
        public DbSet<AuditEvent> AuditEvents { get; set; }

        IQueryable<AuditEvent> ISeedDbContext.AuditEvents
        {
            get { return AuditEvents; }
        }

        public DbSet<User> Users { get; set; }

        IQueryable<User> ISeedDbContext.Users
        {
            get { return Users; }
        }

        public DbSet<Country> Countries { get; set; }

        IQueryable<Country> ISeedDbContext.Countries
        {
            get { return Countries; }
        }

        public Task<int> SaveChangesAsync(IUserContext userContext)
        {
            ApplyInlineAuditValues(userContext);
            RestoreRowVersions();

            return base.SaveChangesAsync();
        }

        public int SaveChanges(IUserContext userContext)
        {
            ApplyInlineAuditValues(userContext);
            RestoreRowVersions();

            return base.SaveChanges();
        }

        private void ApplyInlineAuditValues(IUserContext userContext)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var inlineAudited = entry.Entity as IInlineAudited;

                if (inlineAudited == null) continue;
                
                if (entry.State == EntityState.Added)
                {
                    SetCreated(entry, userContext);
                    SetModified(entry, userContext);
                }
                else if (entry.State == EntityState.Modified)
                {
                    SetModified(entry, userContext);
                }
            }
        }

        private void SetCreated(DbEntityEntry entry, IUserContext userContext)
        {
            entry.CurrentValues["CreatedUtcDate"] = ClockProvider.GetUtcNow();
            entry.CurrentValues["CreatedByUserId"] = userContext.UserId;            
        }

        private void SetModified(DbEntityEntry entry, IUserContext userContext)
        {
             entry.CurrentValues["ModifiedUtcDate"] = ClockProvider.GetUtcNow();
             entry.CurrentValues["ModifiedByUserId"] = userContext.UserId;
        }
        
        private void RestoreRowVersions()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.CurrentValues.PropertyNames.Contains("RowVersion"))
                {
                    var property = entry.Property("RowVersion");

                    property.OriginalValue = property.CurrentValue;
                    property.IsModified = false;
                }
            }
        }
    }
}
