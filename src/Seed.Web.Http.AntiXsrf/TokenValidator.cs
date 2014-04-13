using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Security.Principal;
using System.Web.Http.Controllers;

namespace Seed.Web.Http.AntiXsrf
{
    internal sealed class TokenValidator : ITokenValidator
    {
        private readonly string _cookieTokenName;
        private readonly string _httpHeaderTokenName;
        private readonly IClaimUidExtractor _claimUidExtractor;
        private readonly IAntiForgeryAdditionalDataProvider _additionalDataProvider;
        private readonly bool _suppressIdentityHeuristicChecks;

        internal TokenValidator(
            string cookieTokenName, 
            string httpHeaderTokenName, 
            IClaimUidExtractor claimUidExtractor,
            IAntiForgeryAdditionalDataProvider additionalDataProvider, 
            bool suppressIdentityHeuristicChecks)
        {
            _cookieTokenName = cookieTokenName;
            _httpHeaderTokenName = httpHeaderTokenName;
            _claimUidExtractor = claimUidExtractor;
            _additionalDataProvider = additionalDataProvider;
            _suppressIdentityHeuristicChecks = suppressIdentityHeuristicChecks;
        }

        public AntiForgeryToken GenerateCookieToken()
        {
            return new AntiForgeryToken
            {
                // SecurityToken will be populated automatically.
                IsSessionToken = true
            };
        }

        public AntiForgeryToken GenerateHeaderToken(
            HttpActionContext actionContext, 
            IIdentity identity, 
            AntiForgeryToken cookieToken)
        {
            Contract.Assert(IsCookieTokenValid(cookieToken));

            var headerToken = new AntiForgeryToken
            {
                SecurityToken = cookieToken.SecurityToken,
                IsSessionToken = false
            };

            var requireAuthenticatedUserHeuristicChecks = false;

            // populate Username and ClaimUid
            if (identity != null && identity.IsAuthenticated)
            {
                if (!_suppressIdentityHeuristicChecks)
                {
                    // If the user is authenticated and heuristic checks are not suppressed,
                    // then Username, ClaimUid, or AdditionalData must be set.
                    requireAuthenticatedUserHeuristicChecks = true;
                }

                headerToken.ClaimUid = _claimUidExtractor.ExtractClaimUid(identity);
                
                if (headerToken.ClaimUid == null)
                {
                    headerToken.Username = identity.Name;
                }
            }


            // populate AdditionalData
            if (_additionalDataProvider != null)
            {
                headerToken.AdditionalData = _additionalDataProvider.GetAdditionalData(actionContext);
            }

            if (requireAuthenticatedUserHeuristicChecks
                && String.IsNullOrEmpty(headerToken.Username)
                && headerToken.ClaimUid == null
                && String.IsNullOrEmpty(headerToken.AdditionalData))
            {
                // Application says user is authenticated, but we have no identifier for the user.
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                    StringResources.TokenValidator_AuthenticatedUserWithoutUsername, identity.GetType()));
            }

            return headerToken;
        }

        public bool IsCookieTokenValid(AntiForgeryToken cookieToken)
        {
            return (cookieToken != null && cookieToken.IsSessionToken);
        }

        public void ValidateTokens(
            HttpActionContext actionContext, 
            IIdentity identity, 
            AntiForgeryToken sessionToken, 
            AntiForgeryToken headerToken)
        {
            if (sessionToken == null)
            {
                throw HttpAntiForgeryException.CreateCookieMissingException(_cookieTokenName);
            }

            if (headerToken == null)
            {
                throw HttpAntiForgeryException.CreateHttpHeaderMissingException(_httpHeaderTokenName);
            }

            // Do the tokens have the correct format?
            if (!sessionToken.IsSessionToken || headerToken.IsSessionToken)
            {
                throw HttpAntiForgeryException.CreateTokensSwappedException(_cookieTokenName, _httpHeaderTokenName);
            }

            // Are the security tokens embedded in each incoming token identical?
            if (!Equals(sessionToken.SecurityToken, headerToken.SecurityToken))
            {
                throw HttpAntiForgeryException.CreateSecurityTokenMismatchException();
            }

            // Is the incoming token meant for the current user?
            var currentUsername = String.Empty;
            BinaryBlob currentClaimUid = null;

            if (identity != null && identity.IsAuthenticated)
            {
                currentClaimUid = _claimUidExtractor.ExtractClaimUid(identity);

                if (currentClaimUid == null)
                {
                    currentUsername = identity.Name ?? String.Empty;
                }
            }

            // OpenID and other similar authentication schemes use URIs for the username.
            // These should be treated as case-sensitive.
            var useCaseSensitiveUsernameComparison = currentUsername.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || currentUsername.StartsWith("https://", StringComparison.OrdinalIgnoreCase);

            if (!String.Equals(headerToken.Username, currentUsername, (useCaseSensitiveUsernameComparison) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
            {
                throw HttpAntiForgeryException.CreateUsernameMismatchException(headerToken.Username, currentUsername);
            }

            if (!Equals(headerToken.ClaimUid, currentClaimUid))
            {
                throw HttpAntiForgeryException.CreateClaimUidMismatchException();
            }

            // Is the AdditionalData valid?
            if (_additionalDataProvider != null && !_additionalDataProvider.ValidateAdditionalData(actionContext, headerToken.AdditionalData))
            {
                throw HttpAntiForgeryException.CreateAdditionalDataCheckFailedException();
            }
        }
    }
}
