using System;
using System.Threading.Tasks;
using Seed.Data;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Api.Infrastructure.Messaging
{
    public class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decoreted;
        private readonly ISeedUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand> decoreted,
            ISeedUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _decoreted = decoreted;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<ICommandResult> Handle(TCommand command)
        {
            var result = await _decoreted.Handle(command);

            if (result.Error == null)
            {
                await _unitOfWork.DbContext.SaveChangesAsync(_userContext);
            }

            return result;
        }
    }

    //public class UnitOfWorkCommandBusInterceptor : ICommandInterceptor
    //{
    //    private readonly IComponentContext _container;

    //    private ISeedUnitOfWork _unitOfWork;
    //    private IUserContext _userContext;

    //    public UnitOfWorkCommandBusInterceptor(IComponentContext container)
    //    {
    //        _container = container;
    //    }

    //    public Task PreExecute(ICommandContext context)
    //    {
    //        _unitOfWork = _container.Resolve<ISeedUnitOfWork>();
    //        _userContext = _container.Resolve<IUserContext>();

    //        return TaskHelpers.ForVoidResult();
    //    }

    //    public Task PostExecute(ICommandContext context)
    //    {
    //        if (context.Result.Error != null)
    //        {
    //            return TaskHelpers.ForVoidResult();
    //        }

    //        return _unitOfWork.DbContext.SaveChangesAsync(_userContext);
    //    }

    //    public bool ShouldIntercept(ICommand command)
    //    {
    //        return true;
    //    }
    //}
}
