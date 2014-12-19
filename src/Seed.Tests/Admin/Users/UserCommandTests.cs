using System;
using Seed.Infrastructure.Data;
using Seed.Security;
using Seed.Tests.Data;
using Seed.Tests.Security;

namespace Seed.Tests.Admin.Users
{
    public abstract class UserCommandTests
    {
        private readonly TestSeedDbContext _dbContext;

        protected UserCommandTests()
        {
            _dbContext = new TestSeedDbContext();
        }

        protected ISeedDbContext DbContext
        {
            get { return _dbContext; }
        }

        protected void AddUser(
            Guid id,
            string userName,
            string fullName,
            string email,
            string password,
            bool confirm)
        {
            var passwordHasher = new TestPasswordHasher();

            var user = new User(userName, fullName, email, passwordHasher, password)
            {
                Id = id
            };

            if (confirm)
            {
                user.Confirm();
            }

            _dbContext.Users.Add(user);
        }
    }
}
