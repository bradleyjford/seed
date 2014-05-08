using System;
using System.Linq;
using Seed.Security;

namespace Seed.Api.Admin.Users
{
    public class UserQueryFilter
    {
        public string UserName { get; set; }
        public string FullName { get; set; }

        public IQueryable<User> Apply(IQueryable<User> queryable)
        {
            if (!String.IsNullOrEmpty(UserName))
            {
                queryable = queryable.Where(u => u.UserName.StartsWith(UserName));
            }

            if (!String.IsNullOrEmpty(FullName))
            {
                queryable = queryable.Where(u => u.FullName.StartsWith(FullName));
            }

            return queryable;
        }
    }
}