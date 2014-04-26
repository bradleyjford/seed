using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Seed.Web.Http.AntiXsrf;

namespace Seed.Api.Infrastructure.Filters
{
    public class ApplyAntiForgeryToken : IActionFilter
    {
        private readonly bool _requireSsl;

        public ApplyAntiForgeryToken(bool requireSsl)
        {
            _requireSsl = requireSsl;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation)
        {
            var result = await continuation();

            string newCookieToken, headerToken;
            string oldCookieToken = null;

            var requestCookies = actionContext.Request.Headers.GetCookies(AntiXsrf.CookieName).FirstOrDefault();

            if (requestCookies != null) 
            {
                oldCookieToken = requestCookies[AntiXsrf.CookieName].Value;
            }

            AntiForgery.GetTokens(actionContext, oldCookieToken, out newCookieToken, out headerToken);

            if (newCookieToken != null)
            {
                var serverCookie = new CookieHeaderValue(AntiXsrf.CookieName, newCookieToken)
                {
                    HttpOnly = true,
                    Path = "/",
                    Secure = _requireSsl
                };

                result.Headers.AddCookies(new[] { serverCookie });
            }

            var clientCookie = new CookieHeaderValue("XSRF-TOKEN", headerToken)
            {
                Path = "/",
                Secure = _requireSsl
            };

            result.Headers.AddCookies(new[] { clientCookie });

            return result;
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}