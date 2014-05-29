using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Seed.Common.Auditing;
using Seed.Common.CommandHandling;
using Seed.Common.Security;
using Seed.Data;

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
        private readonly ISeedDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public SignInCommandHandler(ISeedDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<CommandResult<User>> Handle(SignInCommand command)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(
                    u => String.Compare(u.UserName, command.UserName, StringComparison.OrdinalIgnoreCase) == 0);

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
