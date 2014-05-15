﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Seed.Security;

namespace Seed.Data
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

        public Task<User> GetByUserName(string userName)
        {
            return _context.Users.SingleAsync(u => u.UserName == userName);
        }

        public Task<User> GetByLoginProvider(string name, string userKey)
        {
            return _context.Users
                .SingleOrDefaultAsync(u => u.LoginProviders.Any(lp => lp.Name == name && lp.UserKey == userKey));
        }

        public Task Add(User user)
        {
            _context.Users.Add(user);

            return Task.FromResult<object>(null);
        }
    }
}