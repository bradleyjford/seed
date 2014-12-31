using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class RegisterUserCommandValidator : ICommandValidator<RegisterUserCommand>
    {
        private readonly ISeedDbContext _dbContext;

        public RegisterUserCommandValidator(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ValidationResult>> Validate(RegisterUserCommand command)
        {
            var results = new List<ValidationResult>();

            var isUserNameUnique = await IsUserNameUnique(command.UserName);

            if (!isUserNameUnique)
            {
                results.Add(new ValidationResult("The user name specified is already in use. Please choose another.", new [] { "UserName" }));
            }

            if (!PasswordMeetsComplexityRequirements(command.Password))
            {
                results.Add(new ValidationResult("The password specified does not meet the minimum complexity requirements.", new [] { "Password" }));
            }

            return results;
        }

        private async Task<bool> IsUserNameUnique(string userName)
        {
            return await _dbContext.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);
        }

        private bool PasswordMeetsComplexityRequirements(string password)
        {
            return PasswordRequirements.Instance.IsSatisfiedBy(password);
        }
    }
}