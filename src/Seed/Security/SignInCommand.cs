using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Seed.Common.Auditing;
using Seed.Common.CommandHandling;
using Seed.Common.Security;

namespace Seed.Security
{
    public class SignInCommand : ICommand<SignInCommandResult>
    {
        public SignInCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }

        [AuditSensitive]
        public string Password { get; set; }
    }

    public class SignInCommandResult : ICommandResult
    {
        public SignInCommandResult(LoginResult result, User user)
        {
            Success = result.IsSuccessful();
            User = user;
        }

        public SignInCommandResult(Exception error)
        {
            Success = false;
            Error = error;
        }

        public User User { get; private set; }
        public bool Success { get; private set; }
        public Exception Error { get; private set; }
    }

    public class SignInCommandHandler : ICommandHandler<SignInCommand, SignInCommandResult>
    {
        private readonly ISeedDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public SignInCommandHandler(ISeedDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<SignInCommandResult> Handle(SignInCommand command)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(
                 u => String.Compare(u.UserName, command.UserName, StringComparison.OrdinalIgnoreCase) == 0);

            if (user == null)
            {
                return new SignInCommandResult(LoginResult.InvalidUserNameOrPassword, null);
            }

            var loginResult = user.Login(_passwordHasher, command.Password);

            return new SignInCommandResult(loginResult, user);
        }
    }
}
