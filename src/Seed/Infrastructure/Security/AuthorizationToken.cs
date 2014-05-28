using System;
using Seed.Common.Domain;

namespace Seed.Infrastructure.Security
{
    public class AuthorizationToken
    {
        public AuthorizationToken(string secret, DateTime expiryUtcDate)
        {
            Secret = secret;
            ExpiryUtcDate = expiryUtcDate;
        }

        public string Secret { get; private set; }
        public DateTime ExpiryUtcDate { get; private set; }

        public bool IsExpired
        {
            get { return ExpiryUtcDate <= ClockProvider.GetUtcNow(); }
        }

        public void ExpireNow()
        {
            ExpiryUtcDate = ClockProvider.GetUtcNow();
        }
    }
}
