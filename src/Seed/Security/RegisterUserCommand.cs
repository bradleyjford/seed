using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Security;
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

        public RegisterUserCommandHandler(ISeedDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
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
    }
}
