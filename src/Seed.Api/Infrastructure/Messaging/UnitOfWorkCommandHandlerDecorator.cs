using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Data;
using Seed.Security;

namespace Seed.Api.Infrastructure.Messaging
{
    public class UnitOfWorkCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : class
    {
        private readonly ICommandHandler<TCommand, TResult> _decoreted;
        private readonly ISeedUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> decoreted,
            ISeedUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _decoreted = decoreted;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<TResult> Handle(TCommand command)
        {
            var result = await _decoreted.Handle(command);

            await _unitOfWork.DbContext.SaveChangesAsync(_userContext);

            return result;
        }
    }
}
