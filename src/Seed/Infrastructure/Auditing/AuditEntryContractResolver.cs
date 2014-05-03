using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Seed.Infrastructure.Auditing
{
    public class AuditEntryContractResolver : CamelCasePropertyNamesContractResolver
    {
        public static readonly AuditEntryContractResolver Instance = new AuditEntryContractResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var result = base.CreateProperty(member, memberSerialization);

            var attrs = member.GetCustomAttribute<SensitiveAttribute>(true);

            if (attrs != null)
            {
                result.Converter = new MaskedStringJsonConverter();
            }

            return result;
        }
    }
}