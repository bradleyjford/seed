using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class ActivateUserCommand : ICommand<CommandResult>
    {
        public ActivateUserCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }

    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, CommandResult>
    {
        private readonly ISeedDbContext _dbContext;

        public ActivateUserCommandHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(ActivateUserCommand command)
        {
            var user = await _dbContext.Users.FindAsync(command.UserId);

            if (user == null)
            {
                throw new EntityNotFoundException("Could not load User with Id " + command.UserId);
            }

            user.Activate();

            return CommandResult.Ok;
        }
    }
}
