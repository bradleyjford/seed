using System;
using System.Threading.Tasks;
using Seed.Security;

namespace Seed.Data.Admin
{
    public class UserRepository : IUserRepository
    {
        private readonly SeedDbContext _context;

        public UserRepository(ISeedUnitOfWork unitOfWork)
        {
            _context = unitOfWork.DbContext;
        }

        public Task<User> Get(int id)
        {
            return _context.Users.FindAsync(id);
        }
    }
}