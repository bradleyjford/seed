using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Seed.Common.Security;
using Seed.Security;

namespace Seed.Infrastructure.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SeedDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

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

                user.Confirm();

                users.Add(user);
            }

            context.Users.AddOrUpdate(
               u => u.UserName,
               users.ToArray());

            context.SaveChanges();
        }
    }
}
