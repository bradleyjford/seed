using System.Threading.Tasks;
using Seed.Common.CommandHandling;

namespace Seed.Security
{
    public class ActivateUserCommand : ICommand<CommandResult>
    {
        public ActivateUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; protected set; }
    }

    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, CommandResult>
    {
        private readonly IUserRepository _repository;

        public ActivateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(ActivateUserCommand command)
        {
            var user = await _repository.Get(command.UserId);

            user.Activate();

            return CommandResult.Ok;
        }
    }
}
