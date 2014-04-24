using System;
using System.Collections.Generic;
using System.Linq;

namespace Seed.Api.Admin.Users
{
    public class UserRepository
    {
        public static IEnumerable<User> GetAll()
        {
            return new List<User>
                {
                    new User { Id = 1, EmailAddress = "test0@seedapp.io", FullName = "Test 0", UserName = "test0"},
                    new User { Id = 2, EmailAddress = "test1@seedapp.io", FullName = "Test 1", UserName = "test1"},
                    new User { Id = 3, EmailAddress = "test2@seedapp.io", FullName = "Test 2", UserName = "test2"},
                    new User { Id = 4, EmailAddress = "test3@seedapp.io", FullName = "Test 3", UserName = "test3"},
                    new User { Id = 5, EmailAddress = "test4@seedapp.io", FullName = "Test 4", UserName = "test4"},
                    new User { Id = 6, EmailAddress = "test5@seedapp.io", FullName = "Test 5", UserName = "test5"},
                    new User { Id = 7, EmailAddress = "test6@seedapp.io", FullName = "Test 6", UserName = "test6"},
                    new User { Id = 8, EmailAddress = "test7@seedapp.io", FullName = "Test 7", UserName = "test7"},
                    new User { Id = 9, EmailAddress = "test8@seedapp.io", FullName = "Test 8", UserName = "test8"},
                    new User { Id = 10, EmailAddress = "test9@seedapp.io", FullName = "Test 9", UserName = "test9"}
                };
        }

        public static User Get(int id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }
    }
}