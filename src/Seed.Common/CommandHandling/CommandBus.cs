using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Autofac;

namespace Seed.Common.CommandHandling
{
    public partial class CommandBus : ICommandBus
    {
        private readonly IComponentContext _componentContext;

        public CommandBus(IComponentContext container)
        {
            _componentContext = container;
        }

        public virtual Task<TResult> Send<TResult>(ICommand<TResult> command)
            where TResult : class
        {
            var commandType = command.GetType();
            var resultType = typeof(TResult);

            var commandHandlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, resultType);

            var handler = _componentContext.ResolveOptional(commandHandlerType);

            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(commandHandlerType);
            }

            var wrappedType = typeof(CommandHandlerWrapper<,>).MakeGenericType(commandType, resultType);
            var wrapped = (ICommandHandlerWrapper<TResult>)Activator.CreateInstance(wrappedType, handler);

            return wrapped.Handle(command);
        }

        // TODO: Not convinced about the validation implementation
        public virtual IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) 
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
