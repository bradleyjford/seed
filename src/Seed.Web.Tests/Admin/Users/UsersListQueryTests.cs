using System;
using System.Threading.Tasks;
using Seed.Common.Data;
using Seed.Security;
using Seed.Tests.Data;
using Seed.Tests.Security;
using Seed.Web.Handlers.Admin.Users;
using Xunit;

namespace Seed.Tests.Admin.Users
{
    public class UsersListQueryTests
    {
        private readonly TestSeedDbContext _dbContext;
        private readonly IPagingOptions _firstPagePagingOptions;
        private readonly UsersListQueryHandler _queryHandler;

        public UsersListQueryTests()
        {
            _dbContext = new TestSeedDbContext();

            _firstPagePagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 20,
                SortOrder = "FullName asc"
            };

            AddTestUsers();

            _queryHandler = new UsersListQueryHandler(_dbContext);
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
        public async Task Handle_NotFiltered_ReturnsAllUsers()
        {
            var query = new UsersListQuery(String.Empty, _firstPagePagingOptions);

            var users = await _queryHandler.Handle(query);

            Assert.Equal(100, users.ItemCount);
        }

        [Fact]
        public async Task Handle_FilteringByUserName_ReturnsExpectedResults()
        {
            var query = new UsersListQuery( "User 5", _firstPagePagingOptions);

            var users = await _queryHandler.Handle(query);

            Assert.Equal(11, users.ItemCount);
        }
    }
}
