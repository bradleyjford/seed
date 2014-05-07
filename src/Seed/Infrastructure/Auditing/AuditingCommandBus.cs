using System;
using System.Threading.Tasks;
using Autofac;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Infrastructure.Auditing
{
    public class AuditingCommandBus : CommandBus
    {
        private readonly IAuditEntryRepository _repository;
        private readonly IUserContext _userContext;

        public AuditingCommandBus(
            IComponentContext container, 
            IUserContext userContext, 
            IAuditEntryRepository repository) 
            : base(container)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public override async Task<ICommandResult> Submit<TCommand>(TCommand command)
        {
            var result = await base.Submit(command);

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
}
