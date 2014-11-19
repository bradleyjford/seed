using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace Seed.Web.Infrastructure.Middleware.BasicAuthentication
{
    public class BasicAuthenticationMiddleware : 
        AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        public BasicAuthenticationMiddleware(OwinMiddleware next, BasicAuthenticationOptions options) 
            : base(next, options)
        {
        }

        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
        {
            return new BasicAuthenticationHandler(Options);
        }
    }
}