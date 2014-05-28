using System;
using System.Threading.Tasks;
using Seed.Common.Auditing;
using Seed.Common.CommandHandling;
using Seed.Security;

namespace Seed.Admin.Users
{
    public class EditUserCommand : ICommand<CommandResult>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }

        [AuditIgnore]
        public byte[] RowVersion { get; set; }
    }

    public class EditUserCommandHandler : ICommandHandler<EditUserCommand, CommandResult>
    {
        private readonly IUserRepository _repository;

        public EditUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(EditUserCommand command)
        {
            var user = await _repository.Get(command.UserId);

            if (user == null)
            {
                throw new IndexOutOfRangeException();
            }

            user.FullName = command.FullName;
            user.EmailAddress = command.EmailAddress;
            user.Notes = command.Notes;
            user.RowVersion = command.RowVersion;

            return CommandResult.Ok;
        }
    }

}
