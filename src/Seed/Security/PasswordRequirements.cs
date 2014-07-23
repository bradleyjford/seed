using System;
using Seed.Common.Security;
using Seed.Common.Specifications;

namespace Seed.Security
{
    public class PasswordRequirements : AndSpecification<string>
    {
        public static PasswordRequirements Instance = new PasswordRequirements();

        public PasswordRequirements()
            : base(new PasswordLengthRequirement(8, 100), new PasswordComplexityRequirement(1, 1, 1, 1))
        {
        }
    }
}
