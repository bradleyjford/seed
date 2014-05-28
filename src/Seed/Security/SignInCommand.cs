using System;
using System.Threading.Tasks;
using Seed.Common.Auditing;
using Seed.Common.CommandHandling;
using Seed.Common.Security;

namespace Seed.Security
{
    public class SignInCommand : ICommand<CommandResult<User>>
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

    public class SignInCommandHandler : ICommandHandler<SignInCommand, CommandResult<User>>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public SignInCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<CommandResult<User>> Handle(SignInCommand command)
        {
            var user = await _repository.GetByUserName(command.UserName);

            if (user == null)
            {
                return CommandResult<User>.Fail;
            }

            if (_passwordHasher.ValidateHash(user.HashedPassword, command.Password))
            { 
                return new CommandResult<User>(user);
            }

            return CommandResult<User>.Fail;
        }
    }
}
