using System;
using System.Linq;
using System.Threading.Tasks;
using Seed.Infrastructure.CommandHandlerDecorators;
using Seed.Security;
using Seed.Tests.Data;
using Seed.Tests.Security;
using Xunit;

namespace Seed.Tests.Infrastructure.Auditing
{
    public class AuditCommandHandlerDecoratorTests
    {
        private static readonly Guid User1Id = new Guid("00000000-0000-0000-0000-000000000001");
        private static readonly Guid AuditingUserId = new Guid("00000000-0000-0000-0000-000000000002");

        private readonly TestSeedDbContext _dbContext;
        private readonly TestUserContext _userContext;

        public AuditCommandHandlerDecoratorTests()
        {
            _dbContext = new TestSeedDbContext();
            _userContext = new TestUserContext(
                AuditingUserId, "Test User 1", "user1", "test1@test.com", Enumerable.Empty<string>());

            AddUser(User1Id, "test1", "Test User 1", "user1@test.com", "password", true);
        }

        private void AddUser(
            Guid id,
            string userName,
            string fullName,
            string email,
            string password,
            bool confirm)
        {
            var passwordHasher = new TestPasswordHasher();

            var user = new User(userName, fullName, email, passwordHasher, password)
            {
                Id = id
            };

            if (confirm)
            {
                user.Confirm();
            }

            _dbContext.Users.Add(user);
        }

        [Fact(Skip = "Need a way to mock ChangeTracker and DbEntityEntry")]
        public async Task Handle_Test()
        {
            var targetCommand = new TestCommand(User1Id, "Fred");
            var targetCommandHandler = new TestCommandHandler(_dbContext);

            var auditCommandHandler = new AuditCommandHandlerDecorator<TestCommand, string>(
                targetCommandHandler, _dbContext, _userContext);

            var result = await auditCommandHandler.Handle(targetCommand);

            Assert.Equal("OK", result);
        }
    }
}
