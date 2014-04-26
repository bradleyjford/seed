using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand>
    {
        private readonly IUserRepository _repository;

        public ActivateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Execute(ActivateUserCommand command)
        {
            var user = _repository.Get(command.UserId);

            user.Activate();

            return CommandResult.Ok;
        }
    }
}
