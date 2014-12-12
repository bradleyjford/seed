using System;
using System.Data.Entity;
using Seed.Common.Data;
using Seed.Infrastructure.Auditing;
using Seed.Lookups;
using Seed.Security;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Seed.Infrastructure.Data.Mappings
{
    public class UserMapping : EntityMappingConfiguration<User>
    {
        public UserMapping()
        {
            Property(MappingHelpers.GetMember<User, User.UserState>("State"))
                .HasColumnName("State");
        }
    }
}
