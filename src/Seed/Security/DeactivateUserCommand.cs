using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;

namespace Seed.Security
{
    public class DeactivateUserCommand : ICommand<CommandResult>
    {
        public DeactivateUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; protected set; }
    }

    public class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand, CommandResult>
    {
        private readonly IUserRepository _repository;

        public DeactivateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(DeactivateUserCommand command)
        {
            var user = await _repository.Get(command.UserId);

            user.Deactivate();

            return CommandResult.Ok;
        }
    }
}
