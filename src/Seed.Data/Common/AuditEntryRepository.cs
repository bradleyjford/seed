using System;
using Seed.Infrastructure.Auditing;

namespace Seed.Data.Common
{
    public class AuditEntryRepository : IAuditEntryRepository
    {
        private readonly SeedDbContext _context;

        public AuditEntryRepository(ISeedUnitOfWork unitOfWork)
        {
            _context = unitOfWork.DbContext;
        }

        public void Add(AuditEvent auditEvent)
        {
            _context.AuditEvents.Add(auditEvent);
        }
    }
}
