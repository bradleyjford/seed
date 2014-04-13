using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.Web.Http.Controllers;

namespace Seed.Web.Http.AntiXsrf
{
    internal sealed class AntiForgeryWorker
    {
        private readonly IAntiForgeryTokenSerializer _serializer;
        private readonly ITokenValidator _validator;
        private readonly bool _requireSsl;

        internal AntiForgeryWorker(
            IAntiForgeryTokenSerializer serializer, 
            ITokenValidator validator,
            bool requireSsl)
        {
            _serializer = serializer;
            _validator = validator;
            _requireSsl = requireSsl;
        }

        private void CheckSslConfig(HttpActionContext actionContext)
        {
            if (_requireSsl && !IsSSLRequest(actionContext.Request.RequestUri))
            {
                throw new InvalidOperationException(StringResources.AntiForgeryWorker_RequireSSL);
            }
        }

        private bool IsSSLRequest(Uri requestUri)
        {
            return String.Compare(requestUri.Scheme, "https", StringComparison.OrdinalIgnoreCase) == 0;
        }

        private AntiForgeryToken DeserializeToken(string serializedToken)
        {
            return (!String.IsNullOrEmpty(serializedToken))
                ? _serializer.Deserialize(serializedToken)
                : null;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Caller will just regenerate token in case of failure.")]
        private AntiForgeryToken DeserializeTokenNoThrow(string serializedToken)
        {
            try
            {
                return DeserializeToken(serializedToken);
            }
            catch
            {
                // ignore failures since we'll just generate a new token
                return null;
            }
        }

        private static IIdentity ExtractIdentity(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                var user = actionContext.Request.GetUserPrincipal();

                if (user != null)
                {
                    return user.Identity;
                }
            }

            return null;
        }

        /// <summary>
        /// Generates a (cookie, form) serialized token pair for the current user.
        /// The caller may specify an existing cookie value if one exists. If the
        /// 'new cookie value' out param is non-null, the caller *must* persist
        /// the new value to cookie storage since the original value was null or
        /// invalid. This method is side-effect free.
        /// </summary>
        public void GetTokens(HttpActionContext actionContext, string serializedOldCookieToken, out string serializedNewCookieToken, out string serializedFormToken)
        {
            CheckSslConfig(actionContext);

            var oldCookieToken = DeserializeTokenNoThrow(serializedOldCookieToken);
            AntiForgeryToken newCookieToken, headerToken;

            GetTokens(actionContext, oldCookieToken, out newCookieToken, out headerToken);

            serializedNewCookieToken = Serialize(newCookieToken);
            serializedFormToken = Serialize(headerToken);
        }

        private void GetTokens(
            HttpActionContext actionContext, 
            AntiForgeryToken oldCookieToken, 
            out AntiForgeryToken newCookieToken,
            out AntiForgeryToken headerToken)
        {
            newCookieToken = null;

            if (!_validator.IsCookieTokenValid(oldCookieToken))
            {
                // Need to make sure we're always operating with a good cookie token.
                oldCookieToken = newCookieToken = _validator.GenerateCookieToken();
            }

            Contract.Assert(_validator.IsCookieTokenValid(oldCookieToken));

            headerToken = _validator.GenerateHeaderToken(actionContext, ExtractIdentity(actionContext), oldCookieToken);
        }

        private string Serialize(AntiForgeryToken token)
        {
            return (token != null) ? _serializer.Serialize(token) : null;
        }

        /// <summary>
        /// Given the serialized string representations of a cookie & form token,
        /// validates that the pair is OK for this request.
        /// </summary>
        public void Validate(HttpActionContext actionContext, string cookieToken, string headerToken)
        {
            CheckSslConfig(actionContext);

            var deserializedCookieToken = DeserializeToken(cookieToken);
            var deserializedFormToken = DeserializeToken(headerToken);

            _validator.ValidateTokens(actionContext, ExtractIdentity(actionContext), deserializedCookieToken, deserializedFormToken);
        }
    }
}