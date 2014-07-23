using System;
using Seed.Common.Specifications;

namespace Seed.Common.Security
{
    public class PasswordLengthRequirement : ISpecification<string>
    {
        private readonly int _minimumLength;
        private readonly int _maximumLength;

        public PasswordLengthRequirement(int minimumLength, int maximumLength)
        {
            _minimumLength = minimumLength;
            _maximumLength = maximumLength;
        }

        public bool IsSatisfiedBy(string password)
        {
            if (password.Length >= _minimumLength &&
                password.Length <= _maximumLength)
            {
                return true;
            }

            return false;
        }
    }
}
