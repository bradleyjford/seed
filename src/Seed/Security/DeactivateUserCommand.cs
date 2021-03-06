﻿using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class DeactivateUserCommand : ICommand<CommandResult>
    {
        public DeactivateUserCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; protected set; }
    }

    public class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand, CommandResult>
    {
        private readonly ISeedDbContext _dbContext;

        public DeactivateUserCommandHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Handle(DeactivateUserCommand command)
        {
            var user = await _dbContext.Users.FindAsync(command.UserId);

            if (user == null)
            {
                throw new EntityNotFoundException("Could not load User with Id " + command.UserId);
            }

            user.Deactivate();

            return CommandResult.Ok;
        }
    }
}
