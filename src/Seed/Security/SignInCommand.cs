﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Seed.Common.Auditing.Serialization;
using Seed.Common.CommandHandling;
using Seed.Common.Security;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class SignInCommand : ICommand<SignInCommandResult>
    {
        public SignInCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; private set; }

        [AuditSensitive]
        public string Password { get; private set; }
    }

    public class SignInCommandResult : ICommandResult
    {
        public SignInCommandResult(LoginResult result, User user)
        {
            Success = result.IsSuccessful();
            Result = result;
            User = user;
        }

        public SignInCommandResult(Exception error)
        {
            Success = false;
            Error = error;
        }

        public LoginResult Result { get; private set; }
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
            var user = await _dbContext.Users
                .Include("UserClaims")
                .SingleOrDefaultAsync(u => String.Compare(u.UserName, command.UserName, StringComparison.OrdinalIgnoreCase) == 0);

            if (user == null)
            {
                return new SignInCommandResult(LoginResult.InvalidUserNameOrPassword, null);
            }

            var loginResult = user.Login(_passwordHasher, command.Password);

            return new SignInCommandResult(loginResult, user);
        }
    }
}
