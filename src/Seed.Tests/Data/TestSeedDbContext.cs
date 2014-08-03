using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Seed.Common.Data.Testing;
using Seed.Infrastructure.Auditing;
using Seed.Infrastructure.Data;
using Seed.Lookups;
using Seed.Security;

namespace Seed.Tests.Data
{
    public class TestSeedDbContext : ISeedDbContext
    {
        public TestSeedDbContext()
        {            
            AuditEvents = new TestEntityDbSet<AuditEvent, int>();
            Users = new TestEntityDbSet<User, Guid>();
            Countries = new TestEntityDbSet<Country, int>();
        }
        
        public DbSet<AuditEvent> AuditEvents { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<Country> Countries { get; private set; }

        public DbChangeTracker ChangeTracker
        {
            get { throw new NotImplementedException(); }
        }

        public int SaveChangesCount { get; private set; }

        public DbSet Set(Type entityType)
        {
            throw new NotImplementedException();
        }

        public DbSet<T> Set<T>() 
            where T : class
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            SaveChangesCount++;

            return 1;
        }

        public Task<int> SaveChangesAsync()
        {
            SaveChangesCount++;

            return Task.FromResult(1);
        }
    }
}
