using System;
using Seed.Common.Domain;
using Seed.Common.Security;

namespace Seed.Security
{
    public class AuthorizationToken : Entity<Guid>
    {
        protected AuthorizationToken()
        {
            Id = GuidCombIdGenerator.GenerateId();
        }

        public AuthorizationToken(User user, string hashedSecret, DateTime expiryUtcDate)
            : this()
        {
            User = user;
            HashedSecret = hashedSecret;
            ExpiryUtcDate = expiryUtcDate;

            user.AddAuthorizationToken(this);
        }

        public User User { get; private set; }

        public string HashedSecret { get; private set; }
        public DateTime ExpiryUtcDate { get; private set; }
        public DateTime? ConsumedUtcDate { get; private set; }

        public AuthorizationTokenValidationResult Validate(IPasswordHasher passwordHasher, string secret)
        {
            if (IsExpired)
            {
                return AuthorizationTokenValidationResult.Expired;
            }

            if (IsConsumed)
            {
                return AuthorizationTokenValidationResult.PreviouslyConsumed;
            }

            var result = passwordHasher.ValidateHash(HashedSecret, secret);

            if (result)
            {
                return AuthorizationTokenValidationResult.Success;
            }

            return AuthorizationTokenValidationResult.InvalidSecret;
        }

        public bool IsConsumed
        {
            get { return ConsumedUtcDate.HasValue; }
        }

        public bool IsExpired
        {
            get { return ClockProvider.GetUtcNow() > ExpiryUtcDate; }
        }

        public void Consume()
        {
            ConsumedUtcDate = ClockProvider.GetUtcNow();
        }
    }
}
