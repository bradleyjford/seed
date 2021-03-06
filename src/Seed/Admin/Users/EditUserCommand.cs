﻿using System;
using System.Threading.Tasks;
using Seed.Common.Auditing.Serialization;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Infrastructure.Data;

namespace Seed.Admin.Users
{
    public class EditUserCommand : ICommand<CommandResult>
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

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
                throw new EntityNotFoundException("Could not load User with Id " + command.UserId);
            }

            user.FullName = command.FullName;
            user.Email = command.Email;
            user.RowVersion = command.RowVersion;

            return CommandResult.Ok;
        }
    }
}
