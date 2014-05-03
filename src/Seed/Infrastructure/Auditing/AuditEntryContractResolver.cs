using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Seed.Infrastructure.Auditing
{
    public class AuditEntryContractResolver : CamelCasePropertyNamesContractResolver
    {
        public static readonly AuditEntryContractResolver Instance = new AuditEntryContractResolver();

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var result = base.GetSerializableMembers(objectType);

            for (var i = result.Count - 1; i >= 0; i--)
            {
                var member = result[i];

                var auditIgnoreAttribute = member.GetCustomAttribute<AuditIgnoreAttribute>();

                if (auditIgnoreAttribute != null)
                {
                    result.Remove(member);
                }
            }

            return result;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var result = base.CreateProperty(member, memberSerialization);

            var attrs = member.GetCustomAttribute<AuditMaskValue>(true);

            if (attrs != null)
            {
                result.Converter = new MaskedStringJsonConverter();
            }

            return result;
        }
    }
}