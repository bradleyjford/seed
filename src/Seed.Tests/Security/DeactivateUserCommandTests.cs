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
        private static readonly Guid User1Id = new Guid("00000000-0000-0000-0000-000000000001");

        private DeactivateUserCommandHandler _commandHandler;
        private TestSeedDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new TestSeedDbContext();

            AddUser(User1Id, "user1", "Test User 1", "user1@test.com", "password");

            _commandHandler = new DeactivateUserCommandHandler(_dbContext);
        }

        private void AddUser(Guid id, string userName, string fullName, string emailAddress, string password)
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
        public async Task Handle_DeactivatingAnInactiveUser_Succeeds()
        {
            var user = await  _dbContext.Users.FindAsync(User1Id);;

            user.Deactivate();

            var command = new DeactivateUserCommand(User1Id);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.IsActive);
        }

        [Test]
        public async Task Handle_DeactivatingAnActiveUser_Succeeds()
        {
            var user = await _dbContext.Users.FindAsync(User1Id);

            var command = new DeactivateUserCommand(User1Id);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.IsActive);
        }
    }
}
