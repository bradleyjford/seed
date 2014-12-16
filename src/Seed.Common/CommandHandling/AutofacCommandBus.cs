using System;
using Autofac;

namespace Seed.Common.CommandHandling
{
    internal class AutofacCommandBus : CommandBus
    {
        private readonly IComponentContext _container;

        public AutofacCommandBus(IComponentContext container)
        {
            _container = container;
        }

        protected override object GetHandler(Type commandHandlerType)
        {
            return _container.ResolveOptional(commandHandlerType);
        }
    }
}
