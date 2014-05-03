using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seed.Security;

namespace Seed.Tests.Security
{
    public class TestUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User("user1", "Test User 1", "user1@bjf.io")
                {
                    Id = 1
                }
            //new User { Id = 2, Username = "user2", FullName = "Test User 2" , EmailAddress = "user2@bjf.io" },
            //new User { Id = 3, Username = "user3", FullName = "Test User 3" , EmailAddress = "user3@bjf.io" },
            //new User { Id = 4, Username = "user4", FullName = "Test User 4" , EmailAddress = "user4@bjf.io" },
            //new User { Id = 5, Username = "user5", FullName = "Test User 5" , EmailAddress = "user5@bjf.io" },
            //new User { Id = 6, Username = "user6", FullName = "Test User 6" , EmailAddress = "user6@bjf.io" },
            //new User { Id = 7, Username = "user7", FullName = "Test User 7" , EmailAddress = "user7@bjf.io" },
            //new User { Id = 8, Username = "user8", FullName = "Test User 8" , EmailAddress = "user8@bjf.io" },
            //new User { Id = 9, Username = "user9", FullName = "Test User 9" , EmailAddress = "user9@bjf.io" },
            //new User { Id = 10, Username = "user10", FullName = "Test User 10" , EmailAddress = "user10@bjf.io" }
        };

        public Task<User> Get(int id)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
        }
    }
}
