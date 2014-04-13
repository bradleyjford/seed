using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Seed.Web.SpaHost.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ApplyAntiForgeryTokenAttribute : ActionFilterAttribute
    {
        private const string CookieName = "csrf-token";

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                return;
            }

            string newCookieToken, headerToken;
            string oldCookieToken = null;

            var oldCookie = actionExecutedContext.HttpContext.Request.Cookies[CookieName];

            if (oldCookie != null)
            {
                oldCookieToken = oldCookie.Value;
            }

            AntiForgery.GetTokens(oldCookieToken, out newCookieToken, out headerToken);

            if (newCookieToken != null)
            {
                var serverCookie = new HttpCookie(CookieName, newCookieToken)
                {
                    HttpOnly = true,
                    Path = "/"
                };

                actionExecutedContext.HttpContext.Response.AppendCookie(serverCookie);
            }

            var clientCookie = new HttpCookie("XSRF-TOKEN", headerToken)
            {
                Path = "/"
            };

            actionExecutedContext.HttpContext.Response.AppendCookie(clientCookie);
        }
    }
}