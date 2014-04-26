using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}