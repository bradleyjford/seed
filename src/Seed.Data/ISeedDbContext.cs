using System;
using System.Linq;
using Seed.Infrastructure.Auditing;
using Seed.Lookups;
using Seed.Security;

namespace Seed.Data
{
    public interface ISeedDbContext 
    {
        IQueryable<AuditEvent> AuditEvents { get; }
        IQueryable<User> Users { get; }
        IQueryable<Country> Countries { get; }
    }
}