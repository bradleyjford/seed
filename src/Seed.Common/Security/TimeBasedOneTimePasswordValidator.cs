using System;

namespace Seed.Common.Security
{
    public class TimeBasedOneTimePasswordValidator
    {
        public const int GoogleAuthenticatorPasswordLength = 6;

        private readonly OneTimePasswordGenerator _generator;
        private readonly TimeBasedSequenceGenerator _sequenceGenerator;

        public TimeBasedOneTimePasswordValidator()
            : this(GoogleAuthenticatorPasswordLength)
        {
        }

        public TimeBasedOneTimePasswordValidator(int passwordLength)
        {
            _generator =
                new OneTimePasswordGenerator(passwordLength);

            _sequenceGenerator =
                new TimeBasedSequenceGenerator(TimeBasedOneTimePasswordGenerator.ValidityWindow);
        }

        public bool IsValid(byte[] secret, DateTime utcDateTime, string password)
        {
            var sequence = _sequenceGenerator.GetSequence(utcDateTime);

            var result = _generator.Generate(secret, sequence);

            return String.Compare(result, password, StringComparison.InvariantCulture) == 0;
        }
    }
}
