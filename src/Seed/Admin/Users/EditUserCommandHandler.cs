using System;
using System.Threading.Tasks;
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

        public async Task<ICommandResult> Execute(EditUserCommand command)
        {
            var user = await _repository.Get(command.UserId);

            user.FullName = command.FullName;
            user.EmailAddress = command.EmailAddress;
            user.Notes = command.Notes;
            user.RowVersion = command.RowVersion;

            return CommandResult.Ok;
        }
    }
}
