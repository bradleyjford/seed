using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Seed.Api;
using Seed.Api.Infrastructure.Middleware.OAuth2;

[assembly: OwinStartup(typeof(Startup))]

namespace Seed.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = AutofacConfig.Initialize();

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
            var config = new HttpConfiguration();

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),

                Provider = new SeedAuthorizationServerProvider()
            });

            WebApiConfig.Register(config);

            app.UseWebApi(config);

            AutoMapperConfig.Configure();
            
        }
    }
}