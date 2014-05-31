using System;
using Seed.Security;
using Seed.Tests.Data;
using Xunit;

namespace Seed.Tests.Security
{
    public class SignInCommandTests
    {
        private readonly SignInCommandHandler _commandHandler;
        private readonly TestSeedDbContext _dbContext;

        public SignInCommandTests()
        {
            _dbContext = new TestSeedDbContext();

            AddUser(1, "user1", "Test User 1", "user1@test.com", "password");

            _commandHandler = new SignInCommandHandler(_dbContext, new TestPasswordHasher());
        }

        private void AddUser(int id, string userName, string fullName, string emailAddress, string hashedPassword)
        {
            _dbContext.Users.Add(new User(userName, fullName, emailAddress, hashedPassword)
            {
                Id = id
            });
        }

        [Fact]
        public async void Execute_CorrectCredentials_ReturnsSuccess()
        {
            var command = new SignInCommand("user1", "password");

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Execute_IncorrectCredentials_ReturnsFailure()
        {
            var command = new SignInCommand("incorrect", "no-important");

            var result = await _commandHandler.Handle(command);

            Assert.False(result.Success);
        }
    }
}
