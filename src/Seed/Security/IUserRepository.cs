using System;
using System.Threading.Tasks;

namespace Seed.Security
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> Get(int id);
        Task<User> GetByUserName(string userName);

        Task<User> GetByLoginProvider(string name, string userKey);
    }
}