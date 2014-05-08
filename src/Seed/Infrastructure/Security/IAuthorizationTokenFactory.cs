using System;

namespace Seed.Infrastructure.Security
{
    public interface IAuthorizationTokenFactory
    {
        AuthorizationToken Create(TimeSpan validityPeriod, out string secret);
    }
}