using System;

namespace Seed.Common.Text
{
    internal static class Base32
    {
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public const int OutputLengthModulos = 8;
        public const char PaddingCharacter = '=';

        public const int BitsPerByte = 8;
        public const int UInt64ByteCount = sizeof(UInt64);
        public const int BitsPerCharacter = 5;
        public const int BytesPerInputGroup = 5;
    }
}
