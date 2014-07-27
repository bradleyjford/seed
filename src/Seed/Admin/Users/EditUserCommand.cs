using System;
using System.Threading.Tasks;
using Seed.Common.Auditing;
using Seed.Common.CommandHandling;

namespace Seed.Admin.Users
{
    public class EditUserCommand : ICommand<CommandResult>
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }

        [AuditIgnore]
        public byte[] RowVersion { get; set; }
    }

    public class EditUserCommandHandler : ICommandHandler<EditUserCommand, CommandResult>
    {
        private readonly ISeedDbContext _dbContext;

        public EditUserCommandHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(EditUserCommand command)
        {
            var user = await _dbContext.Users.FindAsync(command.UserId);

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
