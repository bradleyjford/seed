using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seed.Infrastructure.Messaging;
using Seed.Infrastructure.Security;

namespace Seed.Security
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ICommandResult> Execute(RegisterUserCommand command)
        {
            var hashedPassword = _passwordHasher.ComputeHash(command.Password);

            var user = new User(command.UserName, command.FullName, command.EmailAddress, hashedPassword);

            await _repository.Add(user);

            return new CommandResult<User>(true, user);
        }
    }
}
