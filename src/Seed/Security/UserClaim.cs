using System;
using System.Security.Claims;
using Seed.Common;
using Seed.Common.Domain;

namespace Seed.Security
{
    public class UserClaim : Entity<Guid>
    {
        protected UserClaim()
        {
            Id = GuidCombIdGenerator.GenerateId();
        }

        public UserClaim(
            string type,
	        string value,
	        string valueType = ClaimValueTypes.String,
	        string issuer = ClaimsIdentity.DefaultIssuer,
	        string originalIssuer = ClaimsIdentity.DefaultIssuer
        ) 
            : this()
        {
            Type = Enforce.ArgumentNotNull("type", type);
            Value = Enforce.ArgumentNotNull("value", value);

            ValueType = valueType;
            Issuer = issuer;
            OriginalIssuer = originalIssuer;
        }

        public string Type { get; protected set; }
        public string Value { get; protected set; }
        public string ValueType { get; protected set; }
        public string Issuer { get; protected set; }
        public string OriginalIssuer { get; protected set; }
    }
}
