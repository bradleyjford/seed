using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Messaging;
using Serilog;

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
        private readonly ILogger _log;

        public ActivateUserCommandHandler(IUserRepository repository, ILogger log)
        {
            _repository = repository;
            _log = log;
        }

        public async Task<ICommandResult> Handle(ActivateUserCommand command)
        {
            var user = await _repository.Get(command.UserId);

            user.Activate();

            _log.Verbose("Activated user");

            return CommandResult.Ok;
        }
    }
}
