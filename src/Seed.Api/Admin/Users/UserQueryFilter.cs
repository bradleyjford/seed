using System;
using System.Linq;
using Seed.Common.Data;
using Seed.Security;

namespace Seed.Api.Admin.Users
{
    public class UserQueryFilter : IQueryFilter<User>
    {
        public string FilterText { get; set; }

        public IQueryable<User> Apply(IQueryable<User> queryable)
        {
            if (!String.IsNullOrEmpty(FilterText))
            {
                queryable = queryable.Where(u =>
                    u.UserName.StartsWith(FilterText) ||
                    u.FullName.StartsWith(FilterText) ||
                    u.EmailAddress.StartsWith(FilterText)
                );
            }

            return queryable;
        }
    }
}