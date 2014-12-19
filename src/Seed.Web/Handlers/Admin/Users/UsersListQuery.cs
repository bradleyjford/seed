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
    public class UsersListQuery : ICommand<IPagedResult<UserSummaryViewModel>>
    {
        public UsersListQuery(string filterText, IPagingOptions pagingOptions)
        {
            FilterText = filterText;
            PagingOptions = pagingOptions;
        }

        public string FilterText { get; private set; }
        public IPagingOptions PagingOptions { get; private set; }
    }

    public class UsersListQueryHandler : ICommandHandler<UsersListQuery, IPagedResult<UserSummaryViewModel>>
    {
        static UsersListQueryHandler()
        {
            Mapper.CreateMap<User, UserSummaryViewModel>();
        }

        private readonly ISeedDbContext _dbContext;

        public UsersListQueryHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IPagedResult<UserSummaryViewModel>> Handle(UsersListQuery query)
        {
            var users = _dbContext.Users.AsQueryable();

            if (!String.IsNullOrEmpty(query.FilterText))
            {
                users = users.Where(u =>
                    u.FullName.StartsWith(query.FilterText) ||
                    u.Email.StartsWith(query.FilterText)
                );
            }

            return await users
                .Project().To<UserSummaryViewModel>()
                .ToPagedResultAsync(query.PagingOptions, new SortDescriptor("FullName"));
        }
    }
}
