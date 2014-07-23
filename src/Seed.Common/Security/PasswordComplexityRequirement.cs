using System;
using Seed.Common.Specifications;

namespace Seed.Common.Security
{
    public class PasswordComplexityRequirement : ISpecification<string>
    {
        private readonly int _minimumDigits;
        private readonly int _minimumSymbols;
        private readonly int _minimumLowercaseLetters;
        private readonly int _minimumUppercaseLetters;

        public PasswordComplexityRequirement(
            int minimumLowercaseLetters,
            int minimumUppercaseLetters,
            int minimumDigits,
            int minimumSymbols)
        {
            _minimumDigits = minimumDigits;
            _minimumSymbols = minimumSymbols;
            _minimumLowercaseLetters = minimumLowercaseLetters;
            _minimumUppercaseLetters = minimumUppercaseLetters;
        }

        public bool IsSatisfiedBy(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            int lowercaseLetterCount;
            int uppercaseLetterCount;
            int digitCount;
            int symbolCount;

            AnalyzePassword(password, out lowercaseLetterCount, out uppercaseLetterCount, out digitCount, out symbolCount);

            if (lowercaseLetterCount < _minimumLowercaseLetters)
            {
                return false;
            }

            if (uppercaseLetterCount < _minimumUppercaseLetters)
            {
                return false;
            }

            if (digitCount < _minimumDigits)
            {
                return false;
            }

            if (symbolCount < _minimumSymbols)
            {
                return false;
            }

            return true;
        }

        private void AnalyzePassword(
            string password,
            out int lowercaseLetterCount,
            out int uppercaseLetterCount,
            out int digitCount,
            out int symbolCount)
        {
            lowercaseLetterCount = 0;
            uppercaseLetterCount = 0;
            digitCount = 0;
            symbolCount = 0;

            foreach (var currentChar in password)
            {
                if (Char.IsLower(currentChar))
                {
                    lowercaseLetterCount++;
                }
                else if (Char.IsUpper(currentChar))
                {
                    uppercaseLetterCount++;
                }
                else if (Char.IsDigit(currentChar))
                {
                    digitCount++;
                }
                else if (Char.IsPunctuation(currentChar) || Char.IsSymbol(currentChar))
                {
                    symbolCount++;
                }
            }
        }
    }
}
