using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Security;
using Seed.Infrastructure.Data;

namespace Seed.Security
{
    public class ConfirmRegistrationCommandValidator : ICommandValidator<ConfirmRegistrationCommand>
    {
        private readonly ISeedDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;

        public ConfirmRegistrationCommandValidator(ISeedDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<ValidationResult>> Validate(ConfirmRegistrationCommand command)
        {
            var results = new List<ValidationResult>();

            var authorizationToken = await _dbContext.AuthorizationTokens.FindAsync(command.TokenId);

            if (authorizationToken == null)
            {
                results.Add(new ValidationResult("Specified token could not be found.", new [] { "TokenId" }));
            }
            else
            {
                var validationResult = authorizationToken.Validate(_passwordHasher, command.Secret);

                if (validationResult != AuthorizationTokenValidationResult.Success)
                {
                    results.Add(new ValidationResult(validationResult.GetErrorMessage()));
                }
            }

            return results;
        }
    }
}
