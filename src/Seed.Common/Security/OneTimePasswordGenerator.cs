using System;
using System.Globalization;
using System.Security.Cryptography;

namespace Seed.Common.Security
{
    public class OneTimePasswordGenerator
    {
        private readonly int _passwordLength;
        private readonly int _passwordModulo;

        public OneTimePasswordGenerator(int passwordLength)
        {
            _passwordLength = passwordLength;

            _passwordModulo = (int)Math.Pow(10, passwordLength);
        }

        public string Generate(byte[] secret, long sequence)
        {
            var hmac = HMAC.Create("HMACSHA1");

            hmac.Key = secret;

            var sequenceBytes = SequenceToByteArray(sequence);

            var hash = hmac.ComputeHash(sequenceBytes);
            var offset = hash[hash.Length - 1] & 0xF;

            var truncatedHash =
                ((hash[offset] & 0x7f) << 24) |
                ((hash[offset + 1] & 0xff) << 16) |
                ((hash[offset + 2] & 0xff) << 8) |
                (hash[offset + 3] & 0xff);

            var password = truncatedHash % _passwordModulo;

            return FormatPassword(password);
        }

        private byte[] SequenceToByteArray(long sequence)
        {
            var result = new byte[8];

            for (var i = result.Length - 1; i >= 0; i--)
            {
                result[i] = (byte)(sequence & 0xff);
                sequence >>= 8;
            }

            return result;
        }

        private string FormatPassword(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture).PadLeft(_passwordLength, '0');
        }
    }
}
