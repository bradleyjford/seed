using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class DeactivateUserCommand : ICommand
    {
        public DeactivateUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; protected set; }
    }

    public class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand>
    {
        private readonly IUserRepository _repository;

        public DeactivateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> Handle(DeactivateUserCommand command)
        {
            var user = await _repository.Get(command.UserId);

            user.Deactivate();

            return CommandResult.Ok;
        }
    }
}
