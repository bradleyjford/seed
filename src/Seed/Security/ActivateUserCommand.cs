using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class ActivateUserCommand : ICommand
    {
        public ActivateUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; protected set; }
    }

    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand>
    {
        private readonly IUserRepository _repository;

        public ActivateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Execute(ActivateUserCommand command)
        {
            var user = await _repository.Get(command.UserId);

            user.Activate();

            return CommandResult.Ok;
        }
    }
}
