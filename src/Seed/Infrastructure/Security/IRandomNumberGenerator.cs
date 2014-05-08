using System;

namespace Seed.Infrastructure.Security
{
	public interface IRandomNumberGenerator
	{
		byte[] Generate(int byteLength);
	}
}
