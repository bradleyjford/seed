using System;
using System.Collections.Generic;
using System.Linq;

namespace Seed.Security
{
    public class UserRepository : IUserRepository
    {
        private static List<User> Users = new List<User>
            {
                new User { Id = 1, EmailAddress = "test0@seedapp.io", FullName = "Test 0", Username = "test0" },
                new User { Id = 2, EmailAddress = "test1@seedapp.io", FullName = "Test 1", Username = "test1" },
                new User { Id = 3, EmailAddress = "test2@seedapp.io", FullName = "Test 2", Username = "test2" },
                new User { Id = 4, EmailAddress = "test3@seedapp.io", FullName = "Test 3", Username = "test3" },
                new User { Id = 5, EmailAddress = "test4@seedapp.io", FullName = "Test 4", Username = "test4" },
                new User { Id = 6, EmailAddress = "test5@seedapp.io", FullName = "Test 5", Username = "test5" },
                new User { Id = 7, EmailAddress = "test6@seedapp.io", FullName = "Test 6", Username = "test6" },
                new User { Id = 8, EmailAddress = "test7@seedapp.io", FullName = "Test 7", Username = "test7" },
                new User { Id = 9, EmailAddress = "test8@seedapp.io", FullName = "Test 8", Username = "test8" },
                new User { Id = 10, EmailAddress = "test9@seedapp.io", FullName = "Test 9", Username = "test9" }
            };

        public IEnumerable<User> GetAll()
        {
            return Users;
        }

        public User Get(int id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }
    }
}