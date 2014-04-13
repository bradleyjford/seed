using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Seed.Web.Http.AntiXsrf
{
    [Serializable]
    public sealed class HttpAntiForgeryException : Exception // TODO: Fix base type
    {
        public HttpAntiForgeryException()
        {
        }

        private HttpAntiForgeryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public HttpAntiForgeryException(string message)
            : base(message)
        {
        }

        private HttpAntiForgeryException(string message, params object[] args)
            : this(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public HttpAntiForgeryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal static HttpAntiForgeryException CreateAdditionalDataCheckFailedException()
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_AdditionalDataCheckFailed);
        }

        internal static HttpAntiForgeryException CreateClaimUidMismatchException()
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_ClaimUidMismatch);
        }

        internal static HttpAntiForgeryException CreateCookieMissingException(string cookieName)
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_CookieMissing, cookieName);
        }

        internal static HttpAntiForgeryException CreateDeserializationFailedException()
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_DeserializationFailed);
        }

        internal static HttpAntiForgeryException CreateHttpHeaderMissingException(string formFieldName)
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_FormFieldMissing, formFieldName);
        }

        internal static HttpAntiForgeryException CreateSecurityTokenMismatchException()
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_SecurityTokenMismatch);
        }

        internal static HttpAntiForgeryException CreateTokensSwappedException(string cookieName, string formFieldName)
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_TokensSwapped, cookieName, formFieldName);
        }

        internal static HttpAntiForgeryException CreateUsernameMismatchException(string usernameInToken, string currentUsername)
        {
            return new HttpAntiForgeryException(StringResources.AntiForgeryToken_UsernameMismatch, usernameInToken, currentUsername);
        }
    }
}
