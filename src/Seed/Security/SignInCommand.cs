using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Auditing;
using Seed.Infrastructure.Messaging;
using Seed.Infrastructure.Security;

namespace Seed.Security
{
    public class SignInCommand : ICommand
    {
        public SignInCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }

        [AuditMaskValue]
        public string Password { get; set; }
    }

    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public SignInCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ICommandResult> Execute(SignInCommand command)
        {
            var user = await _repository.GetByUserName(command.UserName);

            if (user == null)
            {
                return CommandResult.Fail;
            }

            if (_passwordHasher.ValidateHash(user.HashedPassword, command.Password))
            {
                return new CommandResult<User>(true, user);
            }

            return new CommandResult<User>(false, null);
        }
    }
}
