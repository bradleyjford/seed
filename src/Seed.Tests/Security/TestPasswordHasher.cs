using System;
using Seed.Common.Security;

namespace Seed.Tests.Security
{
    public class TestPasswordHasher : IPasswordHasher
    {
        public string ComputeHash(string password)
        {
            return password;
        }

        public bool ValidateHash(string hashedPassword, string password)
        {
            return hashedPassword == password;
        }
    }
}
