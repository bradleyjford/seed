using System;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Text;

namespace Seed.Infrastructure.Security
{
    public class AuthorizationTokenFactory : IAuthorizationTokenFactory
    {
        public static int TokenSizeBytes = 256 / 8;

        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public AuthorizationTokenFactory(
            IRandomNumberGenerator randomNumberGenerator,
            IPasswordHasher passwordHasher)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _passwordHasher = passwordHasher;
        }

        public AuthorizationToken Create(TimeSpan validityPeriod, out string secret)
        {
            var secretBytes = _randomNumberGenerator.Generate(TokenSizeBytes);

            secret = Base32Encoder.Encode(secretBytes);

            var hashedSecret = _passwordHasher.ComputeHash(secret);
            var expiryUtcDate = ClockProvider.GetUtcNow().Add(validityPeriod);

            return new AuthorizationToken(hashedSecret, expiryUtcDate);
        }
    }
}
