using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Autofac;

namespace Seed.Infrastructure.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _componentContext;

        public CommandBus(IComponentContext container)
        {
            _componentContext = container;
        }

        public ICommandResult Submit<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            var handler = _componentContext.Resolve<ICommandHandler<TCommand>>();

            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }

            return handler.Execute(command);
        }

        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            var handler = _componentContext.Resolve<ICommandValidator<TCommand>>();

            if (handler == null)
            {
                throw new CommandValidatorNotFoundException(typeof(TCommand));
            }

            return handler.Validate(command);
        }
    }
}
