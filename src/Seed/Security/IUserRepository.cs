using System;
using System.Threading.Tasks;

namespace Seed.Security
{
    public interface IUserRepository
    {
        Task<User> Get(int id);
    }
}