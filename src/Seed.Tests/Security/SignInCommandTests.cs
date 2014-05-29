using System;
using NUnit.Framework;
using Seed.Security;
using Seed.Tests.Data;

namespace Seed.Tests.Security
{
    [TestFixture]
    public class SignInCommandTests
    {
        private SignInCommandHandler _commandHandler;
        private TestSeedDbContext _dbContext;

        [SetUp]
        public void SetUp()
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
        [Test]
        public async void Execute_CorrectCredentials_ReturnsSuccess()
        {
            var command = new SignInCommand("user1", "password");

            var result = await _commandHandler.Handle(command);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async void Execute_IncorrectCredentials_ReturnsFailure()
        {
            var command = new SignInCommand("incorrect", "no-important");

            var result = await _commandHandler.Handle(command);

            Assert.IsFalse(result.Success);
        }
    }
}
