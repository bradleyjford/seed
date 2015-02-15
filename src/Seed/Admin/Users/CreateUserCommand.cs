using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Seed.Common.Auditing.Serialization;
using Seed.Common.CommandHandling;
using Seed.Common.Security;
using Seed.Infrastructure.Data;
using Seed.Security;

namespace Seed.Admin.Users
{
    public class CreateUserCommand : ICommand<CreateUserCommandResult>
    {
        public string UserName { get; set; }

        [AuditSensitive]
        public string Password { get; set; }

        [AuditSensitive]
        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserCommandResult
    {
        public CreateUserCommandResult(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserCommandResult>
    {
        private readonly ISeedDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(ISeedDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<CreateUserCommandResult> Handle(CreateUserCommand command)
        {
            await EnsureUserNameIsUnique(command);

            EnsurePasswordsMatch(command);

            var user = new User(command.UserName, command.FullName, command.Email, _passwordHasher, command.Password);

            user.Activate();

            return new CreateUserCommandResult(user.Id);
        }

        private static void EnsurePasswordsMatch(CreateUserCommand command)
        {
            if (String.CompareOrdinal(command.Password, command.ConfirmPassword) != 0)
            {
                throw new InvalidOperationException("Password and ConfirmPassword do not match.");
            }
        }

        private async Task EnsureUserNameIsUnique(CreateUserCommand command)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(
                u => String.Compare(u.UserName, command.UserName, StringComparison.OrdinalIgnoreCase) == 0);

            if (user != null)
            {
                throw new InvalidOperationException("UserName is already in use.");
            }
        }
    }
}
