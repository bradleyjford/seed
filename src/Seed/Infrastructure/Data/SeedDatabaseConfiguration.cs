using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Security.Claims;
using Seed.Common.Security;
using Seed.Security;

namespace Seed.Infrastructure.Data
{
    public class SeedDatabaseConfiguration : DbConfiguration
    {
        public SeedDatabaseConfiguration()
        {
            SetDatabaseInitializer(new SeedDatabaseInitializer());
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }

        private class SeedDatabaseInitializer : DropCreateDatabaseIfModelChanges<SeedDbContext>
        {
            protected override void Seed(SeedDbContext context)
            {
                var passwordHasher = new Rfc2898PasswordHasher(
                    Rfc2898PasswordHashParameters.Default,
                    Rfc2898PasswordHashParameters.AllVersions,
                    new RandomNumberGenerator());

                var users = new List<User>();

                for (var i = 1; i <= 100; i++)
                {
                    var user = new User("test" + i, "Test User " + i, "test" + i + "@bjf.io", passwordHasher, "test" + i);

                    if (i % 2 == 0)
                    {
                        user.AddClaim(new UserClaim(ClaimTypes.Role, "admin"));
                    }

                    user.Confirm();

                    users.Add(user);
                }

                context.Users.AddOrUpdate(
                   u => u.Email,
                   users.ToArray());

                context.SaveChanges();
            }
        }
    }
}
