using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Seed.Infrastructure.Domain;
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
            //  This method will be called after migrating to the latest version.

            var userContext = new DummyUserContext();
            var users = new List<User>();

            for (var i = 1; i <= 100; i++)
            {
                var user = new User
                {
                    EmailAddress = "test" + i + "@bjf.io",
                    FullName = "Test User " + i,
                    Username = "test" + i
                };

                user.Initialize(userContext, new SeedClock());

                users.Add(user);
            }

            context.Users.AddOrUpdate(
               u => u.Username,
               users.ToArray());
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
