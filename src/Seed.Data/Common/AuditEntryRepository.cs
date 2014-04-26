using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seed.Common;

namespace Seed.Data.Common
{
    public class AuditEntryRepository : IAuditEntryRepository
    {
        private readonly SeedDbContext _context;

        public AuditEntryRepository(ISeedUnitOfWork unitOfWork)
        {
            _context = unitOfWork.DbContext;
        }

        public void Save(AuditEvent auditEvent)
        {
            _context.AuditEvents.Add(auditEvent);
        }
    }
}
