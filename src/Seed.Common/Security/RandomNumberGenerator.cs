using System;
using System.Security.Cryptography;

namespace Seed.Common.Security
{
	public class RandomNumberGenerator : IRandomNumberGenerator
	{
		private static readonly RNGCryptoServiceProvider CryptoServiceProvider = 
            new RNGCryptoServiceProvider();

		public byte[] Generate(int byteLength)
		{
			var result = new byte[byteLength];

			CryptoServiceProvider.GetBytes(result);

			return result;
		}
	}
}
