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
                    u.FullName.StartsWith(FilterText) ||
                    u.Email.StartsWith(FilterText)
                );
            }

            return queryable;
        }
    }
}