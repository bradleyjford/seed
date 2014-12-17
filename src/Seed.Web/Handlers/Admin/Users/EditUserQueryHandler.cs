using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Data;

namespace Seed.Web.Handlers.Admin.Users
{
    public class EditUserQuery : ICommand<EditUserViewModel>
    {
        public EditUserQuery(Guid userId, EditUserInputModel inputModel)
        {
            UserId = userId;
            InputModel = inputModel;
        }

        public Guid UserId { get; private set; }
        public EditUserInputModel InputModel { get; private set; }
    }

    public class EditUserQueryHandler : ICommandHandler<EditUserQuery, EditUserViewModel>
    {
        private readonly ISeedDbContext _dbContext;

        public EditUserQueryHandler(ISeedDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EditUserViewModel> Handle(EditUserQuery message)
        {
            return new EditUserViewModel();
        }
    }
}