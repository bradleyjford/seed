using System;
using System.Security.Principal;
using System.Web.Http.Controllers;

namespace Seed.Web.Http.AntiXsrf
{
    // Provides an abstraction around something that can validate anti-XSRF tokens
    internal interface ITokenValidator
    {
        // Generates a new random cookie token.
        AntiForgeryToken GenerateCookieToken();

        // Given a cookie token, generates a corresponding form token.
        // The incoming cookie token must be valid.
        AntiForgeryToken GenerateHeaderToken(HttpActionContext actionContext, IIdentity identity, AntiForgeryToken cookieToken);

        // Determines whether an existing cookie token is valid (well-formed).
        // If it is not, the caller must call GenerateCookieToken() before calling GenerateFormToken().
        bool IsCookieTokenValid(AntiForgeryToken cookieToken);

        // Validates a (cookie, form) token pair.
        void ValidateTokens(HttpActionContext actionContext, IIdentity identity, AntiForgeryToken cookieToken, AntiForgeryToken headerToken);
    }
}
