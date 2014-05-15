using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Auditing;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Api.Infrastructure.Messaging
{
    public class AuditCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly IAuditEntryRepository _repository;
        private readonly IUserContext _userContext;

        public AuditCommandHandlerDecorator(
            ICommandHandler<TCommand> decorated,
            IAuditEntryRepository repository,
            IUserContext userContext)
        {
            _decorated = decorated;
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<ICommandResult> Handle(TCommand command)
        {
            var result = await _decorated.Handle(command);

            if (result.Success)
            {
                // TODO: Could do full auditing based on Audit and AuditEntry as per NHibernate
                // inject DbContext and enumerate over ChangeTracker entities.
                var entry = AuditEvent.Create(_userContext, command);

                _repository.Add(entry);
            }

            return result;
        }
    }

    //public class AuditCommandBusInterceptor : ICommandInterceptor
    //{
    //    private readonly IAuditEntryRepository _repository;
    //    private readonly IUserContext _userContext;

    //    public AuditCommandBusInterceptor(IAuditEntryRepository repository, IUserContext userContext)
    //    {
    //        _repository = repository;
    //        _userContext = userContext;
    //    }

    //    public Task PreExecute(ICommandContext context)
    //    {
    //        return TaskHelpers.ForVoidResult();
    //    }

    //    public Task PostExecute(ICommandContext context)
    //    {
    //        if (context.Result.Success)
    //        {
    //            // TODO: Could do full auditing based on Audit and AuditEntry as per NHibernate
    //            // inject DbContext and enumerate over ChangeTracker entities.
    //            var entry = AuditEvent.Create(_userContext, context.Command);

    //            _repository.Add(entry);
    //        }

    //        return TaskHelpers.ForVoidResult();
    //    }

    //    public bool ShouldIntercept(ICommand command)
    //    {
    //        // TODO: Interogate AuditAttribute on command
    //        return true;
    //    }
    //}
}
