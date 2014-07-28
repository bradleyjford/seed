using System;
using Seed.Security;
using Seed.Tests.Data;

namespace Seed.Tests.Security
{
    public abstract class UserCommandTests
    {
        private readonly TestSeedDbContext _dbContext;

        protected UserCommandTests()
        {
            _dbContext = new TestSeedDbContext();
        }

        public ISeedDbContext DbContext
        {
            get { return _dbContext; }
        }

        protected void AddUser(
            Guid id,
            string userName,
            string fullName,
            string emailAddress,
            string password,
            bool confirm)
        {
            var passwordHasher = new TestPasswordHasher();

            var user = new User(userName, fullName, emailAddress, passwordHasher, password)
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
