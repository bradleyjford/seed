using System;
using System.Collections.Generic;

namespace Seed.Infrastructure.Security
{
    public class Rfc2898PasswordHashParameters
    {
        public static readonly Rfc2898PasswordHashParameters Version0 =
            new Rfc2898PasswordHashParameters(0x00, 2500, 128 / 8, 256 / 8);

        public static readonly Rfc2898PasswordHashParameters Default = Version0;

        public static readonly IEnumerable<Rfc2898PasswordHashParameters> AllVersions = new[]
        {
            Version0,
        };

        private readonly byte _version;

        private readonly int _iterations;
        private readonly int _saltSize;
        private readonly int _subKeySize;

        public Rfc2898PasswordHashParameters(byte version, int iterations, int saltSize, int subKeySize)
        {
            _version = version;
            _iterations = iterations;
            _saltSize = saltSize;
            _subKeySize = subKeySize;
        }

        public byte Version
        {
            get { return _version; }
        }

        public int SaltSize
        {
            get { return _saltSize; }
        }

        public int SubKeySize
        {
            get { return _subKeySize; }
        }

        public int KeySize
        {
            get { return 1 + SaltSize + SubKeySize; }
        }

        public int Iterations
        {
            get { return _iterations; }
        }
    }
}

