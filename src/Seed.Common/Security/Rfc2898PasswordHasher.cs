using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Seed.Common.Security
{
    public sealed class Rfc2898PasswordHasher : IPasswordHasher
    {
        private readonly Rfc2898PasswordHashParameters _computeParameters;
        private readonly IEnumerable<Rfc2898PasswordHashParameters> _validationParameters;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public Rfc2898PasswordHasher(
            Rfc2898PasswordHashParameters computeParameters,
            IEnumerable<Rfc2898PasswordHashParameters> validationParameters,
            IRandomNumberGenerator randomNumberGenerator)
        {
            _computeParameters = computeParameters;
            _validationParameters = validationParameters;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string ComputeHash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var saltBytes = _randomNumberGenerator.Generate(_computeParameters.SaltSize);

            byte[] subKey;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, _computeParameters.Iterations))
            {
                subKey = deriveBytes.GetBytes(_computeParameters.SubKeySize);
            }

            var outputBytes = new byte[_computeParameters.KeySize];

            outputBytes[0] = _computeParameters.Version;

            Buffer.BlockCopy(saltBytes, 0, outputBytes, 1, _computeParameters.SaltSize);
            Buffer.BlockCopy(subKey, 0, outputBytes, 1 + _computeParameters.SaltSize, _computeParameters.SubKeySize);

            return Convert.ToBase64String(outputBytes);
        }

        public bool ValidateHash(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException("hashedPassword");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            var parameters = GetParameters(hashedPasswordBytes[0]);

            if (hashedPasswordBytes.Length != parameters.KeySize)
            {
                return false;
            }

            var salt = new byte[parameters.SaltSize];
            var storedSubkey = new byte[parameters.SubKeySize];

            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, parameters.SaltSize);
            Buffer.BlockCopy(hashedPasswordBytes, 1 + parameters.SaltSize, storedSubkey, 0, parameters.SubKeySize);

            byte[] generatedSubkey;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, parameters.Iterations))
            {
                generatedSubkey = deriveBytes.GetBytes(parameters.SubKeySize);
            }

            return storedSubkey.SequenceEqual(generatedSubkey);
        }

        private Rfc2898PasswordHashParameters GetParameters(byte parametersVersion)
        {
            var parameters = _validationParameters.SingleOrDefault(p => p.Version == parametersVersion);

            if (parameters == null)
            {
                throw new InvalidOperationException("Failed to locate hash parameters for specified hash.");
            }

            return parameters;
        }
    }
}
