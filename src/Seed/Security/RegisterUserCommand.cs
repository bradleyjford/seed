using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Net;
using Seed.Common.Security;
using Seed.Infrastructure;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class RegisterUserCommand : ICommand<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, User>
    {
        private readonly ISeedDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISmtpContext _smtpContext;

        public RegisterUserCommandHandler(
            ISeedDbContext dbContext, 
            IPasswordHasher passwordHasher,
            ISmtpContext smtpContext)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _smtpContext = smtpContext;
        }

        public Task<User> Handle(RegisterUserCommand command)
        {
            // Re-validate and throw?

            var user = new User(
                command.UserName, 
                command.FullName, 
                command.EmailAddress, 
                _passwordHasher, 
                command.Password);

            _dbContext.Users.Add(user);

            return Task.FromResult(user);
        }

        private MailMessage PrepareEmailConfirmationMessage(RegisterUserCommand command)
        {
            return new MailMessage();
        }
    }
}
