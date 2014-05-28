using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Auditing;
using Seed.Security;

namespace Seed.Api.Infrastructure.Messaging
{
    public class AuditCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult> 
        where TResult : class
    {
        private readonly ICommandHandler<TCommand, TResult> _decorated;
        private readonly IAuditEntryRepository _repository;
        private readonly IUserContext _userContext;

        public AuditCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> decorated,
            IAuditEntryRepository repository,
            IUserContext userContext)
        {
            _decorated = decorated;
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<TResult> Handle(TCommand command)
        {
            var result = await _decorated.Handle(command);

            // inject DbContext and enumerate over ChangeTracker entities.
            var entry = AuditEvent.Create(_userContext, command);

            _repository.Add(entry);

            return result;
        }
    }
}
