using System;
using Seed.Security;
using Seed.Tests.Data;
using Xunit;

namespace Seed.Tests.Security
{
    public class ActivateUserCommandTests
    {
        private readonly ActivateUserCommandHandler _commandHandler;
        private readonly TestSeedDbContext _dbContext;

        public ActivateUserCommandTests()
        {
            _dbContext = new TestSeedDbContext();

            AddUser(1, "user1", "Test User 1", "user1@test.com", "password");

            _commandHandler = new ActivateUserCommandHandler(_dbContext);
        }

        private void AddUser(int id, string userName, string fullName, string emailAddress, string hashedPassword)
        {
            _dbContext.Users.Add(new User(userName, fullName, emailAddress, hashedPassword)
            {
                Id = id
            });
        }

        [Fact]
        public async void Activate_ActivatingAnActiveUser_Succeeds()
        {
            var userId = 1;

            var command = new ActivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            var user = await _dbContext.Users.FindAsync(userId);

            Assert.True(result.Success);
            Assert.True(user.IsActive);
        }

        [Fact]
        public async void Activate_ActivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = await _dbContext.Users.FindAsync(userId);

            user.Deactivate();

            var command = new ActivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.True(user.IsActive);
        }
    }
}
