using System;
using System.Text;

namespace Seed.Common.Text
{
    public class Base32Encoder
    {
        internal Base32Encoder()
        {
        }

        private readonly byte[] _emptyBuffer = new byte[Base32.UInt64ByteCount];

        public string Encode(byte[] value)
        {
            var outputStringLength =
                Math.Max((int)Math.Ceiling(value.Length * Base32.BitsPerByte / (double)Base32.BitsPerCharacter), 1);

            var outputBuilder = new StringBuilder(outputStringLength);

            var buffer = new byte[Base32.UInt64ByteCount];

            for (var offset = 0; offset < value.Length; offset += Base32.BytesPerInputGroup)
            {
                var length = Math.Min(value.Length - offset, Base32.BytesPerInputGroup);

                var currentValue = GetBigEndianUInt64(value, buffer, offset, length);

                EncodeBigEndianUInt64(outputBuilder, currentValue, length);

                ResetBuffer(buffer);
            }

            ApplyPadding(outputBuilder);

            return outputBuilder.ToString();
        }

        private ulong GetBigEndianUInt64(byte[] data, byte[] buffer, int offset, int count)
        {
            Buffer.BlockCopy(data, offset, buffer, Base32.UInt64ByteCount - (count + 1), count);
            Array.Reverse(buffer);

            return BitConverter.ToUInt64(buffer, 0);
        }

        private void ResetBuffer(byte[] buffer)
        {
            Buffer.BlockCopy(_emptyBuffer, 0, buffer, 0, _emptyBuffer.Length);
        }

        private void EncodeBigEndianUInt64(StringBuilder outputBuilder, ulong value, int availableBytes)
        {
            for (var offset = ((availableBytes + 1) * Base32.BitsPerByte) - Base32.BitsPerCharacter;
                offset > 3;
                offset -= Base32.BitsPerCharacter)
            {
                var index = (int)((value >> offset) & 0x1f);

                outputBuilder.Append(Base32.Alphabet[index]);
            }
        }

        private void ApplyPadding(StringBuilder outputBuilder)
        {
            if (outputBuilder.Length % Base32.OutputLengthModulos > 0)
            {
                var padCount = Base32.OutputLengthModulos - (outputBuilder.Length % Base32.OutputLengthModulos);

                for (var i = 0; i < padCount; i++)
                {
                    outputBuilder.Append(Base32.PaddingCharacter);
                }
            }
        }
    }
}