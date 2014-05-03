using System;

namespace Seed.Infrastructure.Auditing
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public class SensitiveAttribute : Attribute
    {
        
    }
}