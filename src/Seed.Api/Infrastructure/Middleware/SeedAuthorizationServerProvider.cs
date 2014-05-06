using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Seed.Api.Infrastructure.Middleware
{
    public class SeedAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.UserName == "test")
            {
                var identity = new ClaimsIdentity("Embedded");

                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "admin"));

                context.Validated(identity);

                return Task.FromResult<object>(null);
            }

            context.Rejected();

            return Task.FromResult<object>(null);
        }
    }
}