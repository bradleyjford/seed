using System;
using System.Linq;
using System.Threading.Tasks;
using Seed.Common.Domain;
using Seed.Common.Testing;
using Seed.Data.Migrations;
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

        private async Task LockUserAccount(User user)
        {
            var invalidCommand = new SignInCommand("user1", "incorrect-password");

            await _commandHandler.Handle(invalidCommand);
            await _commandHandler.Handle(invalidCommand);
            await _commandHandler.Handle(invalidCommand);
            await _commandHandler.Handle(invalidCommand);
            await _commandHandler.Handle(invalidCommand);

            Assert.True(user.LockedUtcDate.HasValue);
        }

        [Fact]
        public async Task Handle_CorrectCredentials_ReturnsSuccess()
        {
            var command = new SignInCommand("user1", "password");

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Handle_IncorrectCredentials_ReturnsFailure()
        {
            var command = new SignInCommand("incorrect", "no-important");

            var result = await _commandHandler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Handle_SiginingInWithInvalidCredentialsFiveTimes_LocksAccount()
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

        [Fact]
        public async Task Handle_SiginingInWithInvalidCredentialsFourTimes_DoesNotLockAccount()
        {
            var command = new SignInCommand("user1", "incorrect-password");

            await _commandHandler.Handle(command);
            await _commandHandler.Handle(command);
            await _commandHandler.Handle(command);
            await _commandHandler.Handle(command);

            var user = _dbContext.Users.Single();

            Console.WriteLine(user.LockedUtcDate);

            Assert.False(user.LockedUtcDate.HasValue);
        }

        [Fact]
        public async Task Handle_AccountIsLocked_AbleToLoginAfterFiveMinutes()
        {
            var lockUtcDate = new DateTime(2014, 01, 01, 10, 0, 0);

            var user = _dbContext.Users.Single();

            ClockProvider.SetClock(new StaticClock(lockUtcDate));

            await LockUserAccount(user);

            ClockProvider.SetClock(new StaticClock(lockUtcDate.AddMinutes(10).AddSeconds(1)));

            var command = new SignInCommand("user1", "password");

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.LockedUtcDate.HasValue);
        }
    }
}
