using System;

namespace Seed.Infrastructure.Security
{
	public interface IPasswordHasher
	{
		string ComputeHash(string password);
        bool ValidateHash(string hashedPassword, string password);
	}
}
