using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http.Filters;

namespace Seed.Api.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ApplyAntiForgeryTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!actionExecutedContext.Response.IsSuccessStatusCode)
            {
                return;
            }

            string newCookieToken, headerToken;
            string oldCookieToken = null;

            var oldCookie = actionExecutedContext.Request.Headers.GetCookies(AntiXsrf.CookieName).FirstOrDefault();

            if (oldCookie != null)
            {
                oldCookieToken = oldCookie[AntiXsrf.CookieName].Value;
            }

            AntiForgery.GetTokens(oldCookieToken, out newCookieToken, out headerToken);

            if (newCookieToken != null)
            {
                var serverCookie = new CookieHeaderValue(AntiXsrf.CookieName, newCookieToken)
                    {
                        HttpOnly = true
                    };

                var clientCookie = new CookieHeaderValue("XSRF-TOKEN", headerToken);

                actionExecutedContext.Response.Headers.AddCookies(new[] { clientCookie, serverCookie });
            }
        }
    }
}