using System;
using NUnit.Framework;
using Seed.Security;
using Seed.Tests.Data;

namespace Seed.Tests.Security
{
    [TestFixture]
    public class ActivateUserCommandTests
    {
        private ActivateUserCommandHandler _commandHandler;
        private TestSeedDbContext _dbContext;

        [SetUp]
        public void SetUp()
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

        [Test]
        public async void Activate_ActivatingAnActiveUser_Succeeds()
        {
            var userId = 1;

            var command = new ActivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            var user = await _dbContext.Users.FindAsync(userId);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(user.IsActive);
        }

        [Test]
        public async void Activate_ActivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = await _dbContext.Users.FindAsync(userId);

            user.Deactivate();

            var command = new ActivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(user.IsActive);
        }
    }
}
