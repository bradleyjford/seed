using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Seed.Common.Auditing;
using Seed.Common.CommandHandling;
using Seed.Common.Security;

namespace Seed.Security
{
    public class SignInCommand : ICommand<SignInResult>
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

    public class SignInResult : ICommandResult
    {
        public SignInResult(LoginResult result, User user)
        {
            Success = result.IsSuccessful();
            User = user;
        }

        public SignInResult(Exception error)
        {
            Success = false;
            Error = error;
        }

        public User User { get; private set; }
        public bool Success { get; private set; }
        public Exception Error { get; private set; }
    }

    public class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResult>
    {
        private readonly ISeedDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public SignInCommandHandler(ISeedDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<SignInResult> Handle(SignInCommand command)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(
                 u => String.Compare(u.UserName, command.UserName, StringComparison.OrdinalIgnoreCase) == 0);

            if (user == null)
            {
                return new SignInResult(LoginResult.InvalidUserNameOrPassword, null);
            }

            var loginResult = user.Login(_passwordHasher, command.Password);

            return new SignInResult(loginResult, user);
        }
    }
}
