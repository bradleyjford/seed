using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Net;
using Seed.Common.Security;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class RegisterUserCommand : ICommand<RegisterUserCommandResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class RegisterUserCommandResult
    {
        public RegisterUserCommandResult(Guid tokenId, string tokenSecret)
        {
            TokenId = tokenId;
            TokenSecret = tokenSecret;
        }

        public Guid TokenId { get; private set; }
        public string TokenSecret { get; private set; }
    }

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        private readonly ISeedDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthorizationTokenFactory _authorizationTokenFactory;
        private readonly ISmtpContext _smtpContext;

        public RegisterUserCommandHandler(
            ISeedDbContext dbContext, 
            IPasswordHasher passwordHasher,
            IAuthorizationTokenFactory authorizationTokenFactory,
            ISmtpContext smtpContext)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authorizationTokenFactory = authorizationTokenFactory;
            _smtpContext = smtpContext;
        }

        public Task<RegisterUserCommandResult> Handle(RegisterUserCommand command)
        {
            var user = new User(
                command.UserName, 
                command.FullName, 
                command.Email, 
                _passwordHasher, 
                command.Password);

            _dbContext.Users.Add(user);

            string tokenSecret;

            var token = _authorizationTokenFactory.Create(user, TimeSpan.FromDays(1), out tokenSecret);

            return Task.FromResult(new RegisterUserCommandResult(token.Id, tokenSecret));
        }

        private MailMessage PrepareEmailConfirmationMessage(RegisterUserCommand command)
        {
            return new MailMessage();
        }
    }
}
