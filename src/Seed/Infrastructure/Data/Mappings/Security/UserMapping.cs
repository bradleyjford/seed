using System;
using System.Collections.Generic;
using Seed.Common.Data;
using Seed.Common.Data.Mapping;
using Seed.Security;

namespace Seed.Infrastructure.Data.Mappings.Security
{
    internal class UserMapping : AggregateRootEntityMapping<User, Guid, Guid>
    {
        public UserMapping()
            : base("User", "Security")
        {
            Property(MappingHelpers.GetMember<User, User.UserState>("State"))
                .IsRequired();

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            Property(u => u.HashedPassword)
                .IsRequired()
                .HasMaxLength(200);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(300);

            Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);

            HasMany(MappingHelpers.GetMember<User, ICollection<UserClaim>>("UserClaims"))
                .WithRequired()
                .Map(c => c.MapKey("UserId"))
                .WillCascadeOnDelete();

            Ignore(u => u.Claims);

            HasMany(MappingHelpers.GetMember<User, ICollection<AuthorizationToken>>("UserAuthorizationTokens"))
                .WithRequired(t => t.User)
                .Map(c => c.MapKey("UserId"))
                .WillCascadeOnDelete();

            Ignore(u => u.AuthorizationTokens);
        }
    }
}
