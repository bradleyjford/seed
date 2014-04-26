using System;
using Seed.Infrastructure.Messaging;
using Seed.Security;

namespace Seed.Admin.Users
{
    public class EditUserCommandHandler : ICommandHandler<EditUserCommand>
    {
        private readonly UserRepository _repository;

        public EditUserCommandHandler()
        {
            _repository = new UserRepository();
        }

        public ICommandResult Execute(EditUserCommand command)
        {
            var user = _repository.Get(command.Id);

            user.FullName = command.FullName;
            user.EmailAddress = command.EmailAddress;

            return CommandResult.Ok;
        }
    }
}
