using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace Seed.Web.Infrastructure.Middleware.BasicAuthentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private static readonly int BasicHeaderValueLength = "Basic ".Length;

        private readonly string _challenge;

        public BasicAuthenticationHandler(BasicAuthenticationOptions options)
        {
            _challenge = "Basic realm=" + options.Realm;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authorizationHeader = Request.Headers["Authorization"];

            if (String.IsNullOrEmpty(authorizationHeader) ||
                !authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var token = authorizationHeader.Substring(BasicHeaderValueLength).Trim();

            var claims = await TryGetClaims(token, Options.CredentialValidationFunction);

            if (claims == null)
            {
                return null;
            }

            var identity = new ClaimsIdentity(claims, Options.AuthenticationType);

            return new AuthenticationTicket(identity, new AuthenticationProperties());
        }

        private async Task<IEnumerable<Claim>> TryGetClaims(
            string token, 
            CredentialValidationCallback validateCredentials)
        {
            string pair;

            try
            {
                pair = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            }
            catch (Exception)
            {
                return null;
            }

            var seperatorIndex = pair.IndexOf(':');

            if (seperatorIndex == -1)
            {
                return null;
            }

            var username = pair.Substring(0, seperatorIndex);
            var password = pair.Substring(seperatorIndex + 1);

            return await validateCredentials(username, password);
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == 401)
            {
                var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);

                if (challenge == null)
                {
                    Response.Headers.AppendValues("WWW-Authenticate", _challenge);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}