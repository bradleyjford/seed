using System;
using Seed.Infrastructure.Messaging;
using Seed.Security;

namespace Seed.Admin.Users
{
    public class EditUserCommandHandler : ICommandHandler<EditUserCommand>
    {
        private readonly IUserRepository _repository;

        public EditUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Execute(EditUserCommand command)
        {
            var user = _repository.Get(command.UserId);

            user.FullName = command.FullName;
            user.EmailAddress = command.EmailAddress;

            return CommandResult.Ok;
        }
    }
}
