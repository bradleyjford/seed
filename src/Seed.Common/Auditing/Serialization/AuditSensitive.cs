using System;

namespace Seed.Common.Auditing.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AuditSensitive : Attribute
    {
    }
}