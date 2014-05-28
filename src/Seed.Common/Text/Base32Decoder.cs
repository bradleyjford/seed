using System;
using System.Collections.Generic;
using System.IO;

namespace Seed.Common.Text
{
    public class Base32Decoder
    {
        private static readonly Dictionary<string, uint> CharacterMap;

        static Base32Decoder()
        {
            CharacterMap = 
                new Dictionary<string, uint>(Base32.Alphabet.Length, StringComparer.InvariantCultureIgnoreCase);

            for (var i = 0; i < Base32.Alphabet.Length; i++)
            {
                CharacterMap[Base32.Alphabet.Substring(i, 1)] = (uint)i;
            }
        }

        public byte[] Decode(string value)
        {
            if (value.Length % Base32.OutputLengthModulos != 0)
            {
                throw new FormatException("Invalid length for a Base32 encoded string.");
            }

            value = value.TrimEnd('=');

            var decodedLength = 
                Math.Max((int)Math.Ceiling((double)value.Length * Base32.BitsPerCharacter / Base32.BitsPerByte), 1);

            using (var outputStream = new MemoryStream(decodedLength))
            {
                for (var i = 0; i < value.Length; i += 8)
                {
                    var availableCharacters = Math.Min(value.Length - i, Base32.OutputLengthModulos);
                    ulong currentValue = 0;

                    var bytes = 
                        (int)Math.Floor(availableCharacters * (Base32.BitsPerCharacter / (double)Base32.BitsPerByte));

                    for (var offset = 0; offset < availableCharacters; offset++)
                    {
                        uint currentByte;

                        var currentCharacter = value.Substring(i + offset, 1);

                        if (!CharacterMap.TryGetValue(currentCharacter, out currentByte))
                        {
                            throw new FormatException("Invalid character in supplied Base32 encoded string.");
                        }

                        currentValue |= (((ulong)currentByte) << ((((bytes + 1) * 8) - (offset * 5)) - 5));
                    }

                    var buffer = BitConverter.GetBytes(currentValue);

                    Array.Reverse(buffer);

                    outputStream.Write(buffer, buffer.Length - (bytes + 1), bytes);
                }

                return outputStream.ToArray();
            }
        }
    }
}
