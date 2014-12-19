using System;
using System.Threading.Tasks;
using Seed.Security;
using Xunit;

namespace Seed.Tests.Admin.Users
{
    public class ActivateUserCommandTestse : UserCommandTests
    {
        private static readonly Guid User1Id = new Guid("00000000-0000-0000-0000-000000000001");

        private readonly ActivateUserCommandHandler _commandHandler;

        public ActivateUserCommandTestse()
        {
            AddUser(User1Id, "user1", "Test User 1", "user1@test.com", "password", true);

            _commandHandler = new ActivateUserCommandHandler(DbContext);
        }

        [Fact]
        public async Task Handle_ActivatingAnActiveUser_Succeeds()
        {
            var command = new ActivateUserCommand(User1Id);

            var result = await _commandHandler.Handle(command);

            var user = await DbContext.Users.FindAsync(User1Id);

            Assert.True(result.Success);
            Assert.True(user.IsActive);
        }

        [Fact]
        public async Task Handle_ActivatingAnInactiveUser_Succeeds()
        {
            var user = await DbContext.Users.FindAsync(User1Id);

            user.Deactivate();

            var command = new ActivateUserCommand(User1Id);

            var result = await _commandHandler.Handle(command);

            Assert.True(result.Success);
            Assert.True(user.IsActive);
        }
    }
}
