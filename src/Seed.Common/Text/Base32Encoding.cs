using System;

namespace Seed.Common.Text
{
    public static class Base32Encoding
    {
        private static readonly Base32Encoder Encoder = new Base32Encoder();
        private static readonly Base32Decoder Decoder = new Base32Decoder();

        public static string Encode(byte[] value)
        {
            return Encoder.Encode(value);
        }

        public static byte[] Decode(string value)
        {
            return Decoder.Decode(value);
        }
    }
}
