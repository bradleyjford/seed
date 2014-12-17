using System;
using System.Threading.Tasks;
using Seed.Common.Data;
using Seed.Security;
using Seed.Tests.Data;
using Seed.Tests.Security;
using Seed.Web.Handlers.Admin.Users;
using Xunit;

namespace Seed.Web.Tests
{
    public class UsersListQueryTests
    {
        private readonly TestSeedDbContext _dbContext;
        private readonly IPagingOptions _firstPagePagingOptions;

        public UsersListQueryTests()
        {
            _dbContext = new TestSeedDbContext();

            _firstPagePagingOptions = new PagingOptionsInputModel
            {
                PageNumber = 1,
                PageSize = 20,
                SortOrder = "FullName asc"
            };

            AddTestUsers();
        }

        private void AddTestUsers()
        {
            for (var i = 0; i < 100; i++)
            {
                AddUser("user" + i, "password", "user" + i + "@test.com", "User " + i);
            }
        }

        private void AddUser(string userName, string password, string email, string fullName)
        {
            _dbContext.Users.Add(new User(userName, fullName, email, new TestPasswordHasher(), password));
        }

        [Fact]
        public async Task Execute_NotFiltered_ReturnsAllUsers()
        {
            var query = new UsersListQuery(_dbContext, String.Empty, _firstPagePagingOptions);

            var users = await query.Execute();

            Assert.Equal(100, users.ItemCount);
        }

        [Fact]
        public async Task Execute_FilteringByUserName_ReturnsExpectedResults()
        {
            var query = new UsersListQuery(_dbContext, "User 5", _firstPagePagingOptions);

            var users = await query.Execute();

            Assert.Equal(11, users.ItemCount);
        }
    }
}
