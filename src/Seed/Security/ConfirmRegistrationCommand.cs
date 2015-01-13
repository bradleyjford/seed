using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Common.Net;
using Seed.Common.Security;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class ConfirmRegistrationCommand : ICommand<CommandResult>
    {
        public int TokenId { get; set; }
        public string Secret { get; set; }
    }

    public class ConfirmRegistrationCommandHandler : ICommandHandler<ConfirmRegistrationCommand, CommandResult>
    {
        private readonly ISeedDbContext _dbContext;
        private readonly ISmtpContext _smtpContext;
        private readonly IPasswordHasher _passwordHasher;

        public ConfirmRegistrationCommandHandler(
            ISeedDbContext dbContext, 
            ISmtpContext smtpContext,
            IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _smtpContext = smtpContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<CommandResult> Handle(ConfirmRegistrationCommand command)
        {
            var authorizationToken = await _dbContext.AuthorizationTokens.FindAsync(command.TokenId);

            if (authorizationToken == null)
            {
                throw new EntityNotFoundException("Could not load AuthorizationToken with Id " + command.TokenId);
            }

            var user = authorizationToken.User;
            
            user.Confirm(_passwordHasher, authorizationToken, command.Secret);

            // Send confirmation email

            return CommandResult.Ok;
        }
    }
}
