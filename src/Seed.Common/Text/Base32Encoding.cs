using System;

namespace Seed.Common.Text
{
    public class Base32Encoding
    {
        protected const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        protected const int OutputLengthModulos = 8;
        protected const char PaddingCharacter = '=';

        protected const int BitsPerByte = 8;
        protected const int UInt64ByteCount = sizeof(UInt64);
        protected const int BitsPerCharacter = 5;
        protected const int BytesPerInputGroup = 5;

        protected Base32Encoding()
        {
        }
    }
}
