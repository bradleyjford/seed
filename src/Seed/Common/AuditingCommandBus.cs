using System;
using Autofac;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Common
{
    public class AuditingCommandBus : CommandBus
    {
        private readonly IAuditEntryRepository _repository;
        private readonly IUserContext _userContext;

        public AuditingCommandBus(IComponentContext container, IAuditEntryRepository repository) 
            : base(container)
        {
            _repository = repository;

            _userContext = container.Resolve<IUserContext>();
        }

        public override ICommandResult Submit<TCommand>(TCommand command)
        {
            var result = base.Submit(command);

            if (result.Success)
            {
                var entry = AuditEvent.Create(_userContext, command);

                _repository.Save(entry);
            }

            return result;
        }
    }
}
