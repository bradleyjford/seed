using System;
using Seed.Common.Data.Mapping;
using Seed.Infrastructure.Auditing;

namespace Seed.Infrastructure.Data.Mappings.Auditing
{
    internal class AuditEventMapping : EntityMapping<AuditEvent, int>
    {
        public AuditEventMapping() 
            : base("Audit")
        {
        }
    }
}
