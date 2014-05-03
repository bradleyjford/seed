using System;

namespace Seed.Infrastructure.Auditing
{
    public interface IAuditEntryRepository
    {
        void Save(AuditEvent auditEvent);
    }
}
