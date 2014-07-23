using System;
using System.Linq;
using System.Threading.Tasks;
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

        private void AddUser(int id, string userName, string fullName, string emailAddress, string password)
        {
            var passwordHasher = new TestPasswordHasher();

            var user = new User(userName, fullName, emailAddress, passwordHasher, password)
            {
                Id = id
            };

            user.Confirm();

            _dbContext.Users.Add(user);
        }

        [Fact]
        public async Task Execute_CorrectCredentials_ReturnsSuccess()
        {
            var command = new SignInCommand("user1", "password");

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Execute_IncorrectCredentials_ReturnsFailure()
        {
            var command = new SignInCommand("incorrect", "no-important");

            var result = await _commandHandler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Execute_SiginingInWithInvalidCredentials5Times_LocksAccount()
        {
            var command = new SignInCommand("user1", "incorrect-password");

            await _commandHandler.Handle(command);
            await _commandHandler.Handle(command);
            await _commandHandler.Handle(command);
            await _commandHandler.Handle(command);
            await _commandHandler.Handle(command);

            var user = _dbContext.Users.Single();

            Console.WriteLine(user.LockedUtcDate);

            Assert.True(user.LockedUtcDate.HasValue);
        }
    }
}
