using System;
using Seed.Common.Data.Mapping;
using Seed.Security;

namespace Seed.Infrastructure.Data.Mappings.Security
{
    internal class AuthorizationTokenMapping : EntityMapping<AuthorizationToken, Guid>
    {
        public AuthorizationTokenMapping() 
            : base("AuthorizationToken", "Security")
        {
            Property(p => p.HashedSecret)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
