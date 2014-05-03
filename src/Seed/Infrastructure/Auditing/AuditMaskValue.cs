  using System;

namespace Seed.Infrastructure.Auditing
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AuditMaskValue : Attribute
    {
        
    }
}