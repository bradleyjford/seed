using System;

namespace Seed.Infrastructure.Auditing
{
    public interface IAuditEntryRepository
    {
        void Add(AuditEvent auditEvent);
    }
}
