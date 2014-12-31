using System;
using Seed.Common.Data.Mapping;
using Seed.Security;

namespace Seed.Infrastructure.Data.Mappings.Security
{
    internal class UserClaimMapping : EntityMapping<UserClaim, Guid>
    {
        public UserClaimMapping()
            : base("Claim", "Security")
        {
            Property(p => p.Type)
                .IsRequired();

            Property(p => p.Value)
                .IsRequired();
        }
    }
}
