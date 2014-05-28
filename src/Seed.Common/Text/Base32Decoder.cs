using System;
using System.Collections.Generic;
using System.IO;

namespace Seed.Common.Text
{
	public class Base32Decoder : Base32Encoding
	{
		private static readonly Dictionary<string, uint> CharacterMap;

		static Base32Decoder()
		{
			CharacterMap = new Dictionary<string, uint>(Alphabet.Length, StringComparer.InvariantCultureIgnoreCase);

			for (var i = 0; i < Alphabet.Length; i++)
			{
				CharacterMap[Alphabet.Substring(i, 1)] = (uint)i;
			}
		}

		private Base32Decoder()
		{
		}

		public static byte[] Decode(string value)
		{
			if (value.Length % OutputLengthModulos != 0)
			{
				throw new FormatException("Invalid length for a Base32 encoded string.");
			}

			value = value.TrimEnd('=');

			var decodedLength = Math.Max((int)Math.Ceiling((double)value.Length * BitsPerCharacter / BitsPerByte), 1);

			using (var outputStream = new MemoryStream(decodedLength))
			{
				for (var i = 0; i < value.Length; i += 8)
				{
					var availableCharacters = Math.Min(value.Length - i, OutputLengthModulos);
					ulong currentValue = 0;
					
					var bytes = (int)Math.Floor(availableCharacters * (BitsPerCharacter / (double)BitsPerByte));

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
