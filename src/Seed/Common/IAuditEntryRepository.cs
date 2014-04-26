using System;

namespace Seed.Common
{
    public interface IAuditEntryRepository
    {
        void Save(AuditEvent auditEvent);
    }
}
