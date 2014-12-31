using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security.OAuth;
using Seed.Common.CommandHandling;
using Seed.Security;

namespace Seed.Api.Infrastructure.Middleware
{
    public class SeedAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var container = context.OwinContext.GetAutofacLifetimeScope();

            var bus = container.Resolve<ICommandBus>();
            
            var command = new SignInCommand(context.UserName, context.Password);

            var result = await bus.Execute(command);

            if (result.Success)
            {
                var user = result.User;

                var identity = new ClaimsIdentity("Seed", ClaimTypes.NameIdentifier, ClaimTypes.Role);

                foreach (var claim in user.Claims)
                {
                    identity.AddClaim(new Claim(claim.Type, claim.Value, claim.ValueType, claim.Issuer, claim.OriginalIssuer, identity));
                }

                identity.AddClaim(new Claim("SeedUserId", user.Id.ToString(), ClaimValueTypes.HexBinary));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                context.Validated(identity);

                return;
            }

            context.Rejected();
        }
    }
}
