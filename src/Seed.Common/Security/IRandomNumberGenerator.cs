using System;

namespace Seed.Common.Security
{
    public interface IRandomNumberGenerator
    {
        byte[] Generate(int byteLength);
    }
}
