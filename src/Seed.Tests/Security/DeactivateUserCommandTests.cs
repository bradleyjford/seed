using System;
using Seed.Security;
using Seed.Tests.Data;
using Xunit;

namespace Seed.Tests.Security
{
    public class DectivateUserCommandTests
    {
        private readonly DeactivateUserCommandHandler _commandHandler;
        private readonly TestSeedDbContext _dbContext;

        public DectivateUserCommandTests()
        {
            _dbContext = new TestSeedDbContext();

            AddUser(1, "user1", "Test User 1", "user1@test.com", "password");

            _commandHandler = new DeactivateUserCommandHandler(_dbContext);
        }

        private void AddUser(int id, string userName, string fullName, string emailAddress, string hashedPassword)
        {
            _dbContext.Users.Add(new User(userName, fullName, emailAddress, hashedPassword)
            {
                Id = id
            });
        }

        [Fact]
        public async void Execute_DeactivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = await  _dbContext.Users.FindAsync(userId);;

            user.Deactivate();

            var command = new DeactivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.IsActive);
        }

        [Fact]
        public async void Execute_DeactivatingAnActiveUser_Succeeds()
        {
            var userId = 1;
            var user = await _dbContext.Users.FindAsync(userId);

            var command = new DeactivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.IsActive);
        }
    }
}
