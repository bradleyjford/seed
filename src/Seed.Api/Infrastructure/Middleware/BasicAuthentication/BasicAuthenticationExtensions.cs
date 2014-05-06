using System;
using Owin;

namespace Seed.Api.Infrastructure.Middleware.BasicAuthentication
{
    public static class BasicAuthenticationExtensions
    {
        public static IAppBuilder UseBasicAuthentication(
            this IAppBuilder app, 
            string realm, 
            CredentialValidationCallback credentialValidationCallback)
        {
            var options = new BasicAuthenticationOptions(realm, credentialValidationCallback);

            return app.UseBasicAuthentication(options);
        }

        public static IAppBuilder UseBasicAuthentication(
            this IAppBuilder app,
            BasicAuthenticationOptions options)
        {
            return app.Use<BasicAuthenticationMiddleware>(options);
        }
    }
}