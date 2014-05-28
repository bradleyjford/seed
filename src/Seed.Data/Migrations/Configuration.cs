using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Seed.Common.Domain;
using Seed.Common.Security;
using Seed.Security;

namespace Seed.Data.Migrations
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

            var userContext = new DummyUserContext();
            var users = new List<User>();

            for (var i = 1; i <= 100; i++)
            {
                var password = passwordHasher.ComputeHash("test" + i);

                users.Add(new User("test" + i, "Test User " + i, "test" + i + "@bjf.io", password));
            }

            context.Users.AddOrUpdate(
               u => u.UserName,
               users.ToArray());

            context.SaveChanges(userContext);
        }

        public class SeedClock : IClock
        {
            public DateTime GetUtcNow()
            {
                return DateTime.Today;
            }
        }

        public class DummyUserContext : IUserContext
        {
            public int UserId { get { return 1; } }
            
            public string DisplayName { get; private set; }
            public string UserName { get; private set; }
            public string EmailAddress { get; private set; }

            public bool IsInRole(string role)
            {
                throw new NotImplementedException();
            }
        }
    }
}
