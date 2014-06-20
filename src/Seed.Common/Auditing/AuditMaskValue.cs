using System;

namespace Seed.Common.Auditing
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class AuditMaskValue : Attribute
    {
    }
}