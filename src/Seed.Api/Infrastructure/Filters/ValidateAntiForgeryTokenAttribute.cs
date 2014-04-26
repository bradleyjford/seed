using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Seed.Web.Http.AntiXsrf;

namespace Seed.Api.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateAntiForgeryTokenAttribute : AuthorizationFilterAttribute
    {
        private const string HeaderName = "X-XSRF-TOKEN";

        private readonly string _headerName;

        public ValidateAntiForgeryTokenAttribute(string headerName = HeaderName)
        {
            _headerName = headerName;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var cookieToken = "";
            var headerToken = "";

            IEnumerable<string> tokenHeaders;

            if (actionContext.Request.Headers.TryGetValues(_headerName, out tokenHeaders))
            {
                headerToken = tokenHeaders.First().Trim();
            }

            var cookie = actionContext.Request.Headers.GetCookies(AntiXsrf.CookieName).FirstOrDefault();

            if (cookie != null)
            {
                cookieToken = cookie[AntiXsrf.CookieName].Value;
            }

            try
            {
                AntiForgery.Validate(actionContext, cookieToken, headerToken);
            }
            catch (HttpAntiForgeryException ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message)
                };

                actionContext.Response = response;
            }
        }
    }
}