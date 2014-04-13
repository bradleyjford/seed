using System;
using System.Web.Http.Controllers;

namespace Seed.Web.Http.AntiXsrf
{
    /// <summary>
    /// Provides an abstraction around how tokens are persisted and retrieved for a request
    /// </summary>
    internal interface ITokenStore
    {
        AntiForgeryToken GetCookieToken(HttpActionContext actionContext);
        AntiForgeryToken GetFormToken(HttpActionContext actionContext);
        void SaveCookieToken(HttpActionContext actionContext, AntiForgeryToken token);
    }
}
