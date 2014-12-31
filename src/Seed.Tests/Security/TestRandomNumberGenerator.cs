using System;
using Seed.Common.Security;

namespace Seed.Tests.Security
{
    public class TestRandomNumberGenerator : IRandomNumberGenerator
    {
        public byte[] Generate(int byteLength)
        {
            return new byte[byteLength];
        }
    }
}
