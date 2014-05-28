using System;

namespace Seed.Common.Security
{
    public interface IPasswordHasher
    {
        string ComputeHash(string password);
        bool ValidateHash(string hashedPassword, string password);
    }
}
