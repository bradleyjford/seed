using System;
using System.Threading.Tasks;
using Autofac;
using Seed.Data;
using Seed.Infrastructure.Auditing;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Api.Infrastructure.Messaging
{
    public class SeedCommandBus : AuditingCommandBus
    {
        private readonly IComponentContext _container;

        public SeedCommandBus(
            IComponentContext container, 
            IAuditEntryRepository repository) 
            : base(container, repository)
        {
            _container = container;
        }

        public override async Task<ICommandResult> Submit<TCommand>(TCommand command)
        {
            var unitOfWork = _container.Resolve<ISeedUnitOfWork>();
            var userContext = _container.Resolve<IUserContext>();

            var result = await base.Submit(command);

            await unitOfWork.DbContext.SaveChangesAsync(userContext);

            return result;
        }
    }
}