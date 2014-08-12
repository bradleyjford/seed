using System;

namespace Seed.Common.Security
{
    public class TimeBasedOneTimePasswordGenerator
    {
        public static readonly int ValidityWindowSeconds = 30;

        private readonly OneTimePasswordGenerator _passwordGenerator;
        private readonly TimeBasedSequenceGenerator _sequenceGenerator;

        public TimeBasedOneTimePasswordGenerator(int passwordLength)
        {
            _passwordGenerator = new OneTimePasswordGenerator(passwordLength);
            _sequenceGenerator = new TimeBasedSequenceGenerator(ValidityWindowSeconds);
        }

        public string Generate(byte[] secret, DateTime utcDateTime)
        {
            var sequence = _sequenceGenerator.GetSequence(utcDateTime);

            return _passwordGenerator.Generate(secret, sequence);
        }

        public TimeBasedSequenceGenerator SequenceGenerator
        {
            get { return _sequenceGenerator; }
        }
    }
}
