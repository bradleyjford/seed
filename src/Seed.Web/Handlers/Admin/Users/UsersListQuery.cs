using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Seed.Common.CommandHandling;
using Seed.Common.Data;
using Seed.Infrastructure.Data;
using Seed.Security;

namespace Seed.Web.Handlers.Admin.Users
{
    public class UsersListQuery : IQuery<IPagedResult<UserSummaryViewModel>>
    {
        static UsersListQuery()
        {
            Mapper.CreateMap<User, UserSummaryViewModel>();
        }

        private readonly ISeedDbContext _dbContext;
        private readonly string _filterText;
        private readonly IPagingOptions _pagingOptions;

        public UsersListQuery(ISeedDbContext dbContext, string filterText, IPagingOptions pagingOptions)
        {
            _dbContext = dbContext;
            _filterText = filterText;
            _pagingOptions = pagingOptions;
        }

        public async Task<IPagedResult<UserSummaryViewModel>> Execute()
        {
            var users = _dbContext.Users.AsQueryable();

            if (!String.IsNullOrEmpty(_filterText))
            {
                users = users.Where(u =>
                    u.FullName.StartsWith(_filterText) ||
                    u.Email.StartsWith(_filterText)
                );
            }

            return await users
                .Project().To<UserSummaryViewModel>()
                .ToPagedResultAsync(_pagingOptions, new SortDescriptor("FullName"));
        }
    }
}
