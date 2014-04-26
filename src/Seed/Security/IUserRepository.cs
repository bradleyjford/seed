using System;
using System.Collections.Generic;

namespace Seed.Security
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
    }
}