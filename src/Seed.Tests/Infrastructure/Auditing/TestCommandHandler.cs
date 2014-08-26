using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Data;

namespace Seed.Tests.Infrastructure.Auditing
{
    public class TestCommand : ICommand<string>
    {
        public TestCommand(Guid userId, string newName)
        {
            UserId = userId;
            NewName = newName;
        }

        public Guid UserId { get; private set; }
        public string NewName { get; set; }
    }

    public class TestCommandHandler : ICommandHandler<TestCommand, string>
    {
        private readonly ISeedDbContext _dbContext;

        public TestCommandHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(TestCommand command)
        {
            var user = await _dbContext.Users.FindAsync(command.UserId);

            user.FullName = command.NewName;

            return "OK";
        }
    }
}
