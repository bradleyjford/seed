using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security.OAuth;
using Seed.Common.CommandHandling;
using Seed.Security;

namespace Seed.Web.Infrastructure.Middleware.OAuth2
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

            var result = await bus.Send(command);

            if (result.Success)
            {
                var user = result.User;

                var identity = new ClaimsIdentity("Seed");

                identity.AddClaim(new Claim("SeedUserId", user.Id.ToString(), ClaimValueTypes.Integer32));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));

                context.Validated(identity);

                return;
            }

            context.Rejected();
        }
    }
}
