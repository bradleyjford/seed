using System;
using System.Threading.Tasks;
using Seed.Security;
using Xunit;

namespace Seed.Tests.Admin.Users
{
    public class DectivateUserCommandTests : UserCommandTests
    {
        private static readonly Guid User1Id = new Guid("00000000-0000-0000-0000-000000000001");

        private readonly DeactivateUserCommandHandler _commandHandler;

        public DectivateUserCommandTests()
        {
            AddUser(User1Id, "user1", "Test User 1", "user1@test.com", "password", true);

            _commandHandler = new DeactivateUserCommandHandler(DbContext);
        }

        [Fact]
        public async Task Handle_DeactivatingAnInactiveUser_Succeeds()
        {
            var user = await DbContext.Users.FindAsync(User1Id);;

            user.Deactivate();

            var command = new DeactivateUserCommand(User1Id);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.IsActive);
        }

        [Fact]
        public async Task Handle_DeactivatingAnActiveUser_Succeeds()
        {
            var user = await DbContext.Users.FindAsync(User1Id);

            var command = new DeactivateUserCommand(User1Id);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.False(user.IsActive);
        }
    }
}
