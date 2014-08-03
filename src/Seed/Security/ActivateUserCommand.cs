﻿using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class ActivateUserCommand : ICommand<CommandResult>
    {
        public ActivateUserCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }

    public class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, CommandResult>
    {
        private readonly ISeedDbContext _dbContext;

        public ActivateUserCommandHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(ActivateUserCommand command)
        {
            var user = await _dbContext.Users.FindAsync(command.UserId);

            if (user == null)
            {
                throw new IndexOutOfRangeException();
            }

            user.Activate();

            return CommandResult.Ok;
        }
    }
}
