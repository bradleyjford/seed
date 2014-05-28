using System;
using System.Text;

namespace Seed.Common.Text
{
    public class Base32Encoder : Base32Encoding
    {
        private Base32Encoder()
        {
        }

        private static readonly byte[] EmptyBuffer = new byte[UInt64ByteCount];

        public static string Encode(byte[] value)
        {
            var outputStringLength = Math.Max((int)Math.Ceiling(value.Length * BitsPerByte / (double)BitsPerCharacter),
                1);

            var outputBuilder = new StringBuilder(outputStringLength);

            var buffer = new byte[UInt64ByteCount];

            for (var offset = 0; offset < value.Length; offset += BytesPerInputGroup)
            {
                var length = Math.Min(value.Length - offset, BytesPerInputGroup);

                var currentValue = GetBigEndianUInt64(value, buffer, offset, length);

                EncodeBigEndianUInt64(outputBuilder, currentValue, length);

                ResetBuffer(buffer);
            }

            ApplyPadding(outputBuilder);

            return outputBuilder.ToString();
        }

        private static ulong GetBigEndianUInt64(byte[] data, byte[] buffer, int offset, int count)
        {
            Buffer.BlockCopy(data, offset, buffer, UInt64ByteCount - (count + 1), count);
            Array.Reverse(buffer);

            return BitConverter.ToUInt64(buffer, 0);
        }

        private static void ResetBuffer(byte[] buffer)
        {
            Buffer.BlockCopy(EmptyBuffer, 0, buffer, 0, EmptyBuffer.Length);
        }

        private static void EncodeBigEndianUInt64(StringBuilder outputBuilder, ulong value, int availableBytes)
        {
            for (var offset = ((availableBytes + 1) * BitsPerByte) - BitsPerCharacter;
                offset > 3;
                offset -= BitsPerCharacter)
            {
                var index = (int)((value >> offset) & 0x1f);

                outputBuilder.Append(Alphabet[index]);
            }
        }

        private static void ApplyPadding(StringBuilder outputBuilder)
        {
            if (outputBuilder.Length % OutputLengthModulos > 0)
            {
                var padCount = OutputLengthModulos - (outputBuilder.Length % OutputLengthModulos);

                for (var i = 0; i < padCount; i++)
                {
                    outputBuilder.Append(PaddingCharacter);
                }
            }
        }
    }
}