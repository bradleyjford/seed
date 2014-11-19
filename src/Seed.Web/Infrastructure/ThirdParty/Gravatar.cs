using System;
using System.Security.Cryptography;
using System.Text;

namespace Seed.Web.Infrastructure.ThirdParty
{
    public static class Gravatar
    {
        private const string UrlFormat = "{0}.gravatar.com/{1}";

        private static readonly MD5 Hasher = MD5.Create();

        public static string GetUrl(string email, int size, bool https)
        {
            var hash = CalculateHash(email);

            var scheme = "http://www";

            if (https)
            {
                scheme = "https://secure";
            }

            return String.Format(UrlFormat, scheme, hash);
        }

        private static string CalculateHash(string email)
        {
            var rawBytes = Encoding.UTF8.GetBytes(email.Trim().ToLowerInvariant());

            var data = Hasher.ComputeHash(rawBytes);

            var result = new StringBuilder();

            for (var i = 0; i < data.Length; i++)
            {
                result.Append(data[i].ToString("x2"));
            }

            return result.ToString();
        }
    }
}
