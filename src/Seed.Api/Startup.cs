using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Seed.Api;
using Seed.Api.Infrastructure.Middleware;
using Seed.Api.Infrastructure.Middleware.BasicAuthentication;

[assembly: OwinStartup(typeof(Startup))]

namespace Seed.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            { 
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
 
                Provider = new SeedAuthorizationServerProvider()
            });
 
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var config = new HttpConfiguration();

            AutoMapperConfig.Configure();

            WebApiConfig.Register(config);
            AutofacConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}