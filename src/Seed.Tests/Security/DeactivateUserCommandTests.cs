using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Seed.Security;
using Seed.Tests.Data;

namespace Seed.Tests.Security
{
    [TestFixture]
    public class DectivateUserCommandTests
    {
        private DeactivateUserCommandHandler _commandHandler;
        private TestSeedDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new TestSeedDbContext();

            AddUser(1, "user1", "Test User 1", "user1@test.com", "password");

            _commandHandler = new DeactivateUserCommandHandler(_dbContext);
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

        [Test]
        public async Task Execute_DeactivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = await  _dbContext.Users.FindAsync(userId);;

            user.Deactivate();

            var command = new DeactivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.IsActive);
        }

        [Test]
        public async Task Execute_DeactivatingAnActiveUser_Succeeds()
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
