using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public abstract partial class CommandBus : ICommandBus
    {
        public virtual Task<TResult> Invoke<TResult>(ICommand<TResult> command)
            where TResult : class
        {
            var commandType = command.GetType();
            var resultType = typeof(TResult);

            var commandHandlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, resultType);

            var handler = GetHandler(commandHandlerType);

            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(commandHandlerType);
            }

            var wrappedType = typeof(CommandHandlerWrapper<,>).MakeGenericType(commandType, resultType);
            var wrapped = (ICommandHandlerWrapper<TResult>)Activator.CreateInstance(wrappedType, handler);
                
            return wrapped.Handle(command);
        }

        protected abstract object GetHandler(Type commandHandlerType);

        public virtual async Task<IEnumerable<ValidationResult>> Validate<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            var handler = (ICommandValidator<TCommand>)GetHandler(typeof(ICommandValidator<TCommand>));

            if (handler == null)
            {
                throw new CommandValidatorNotFoundException(typeof(TCommand));
            }

            return await handler.Validate(command);
        }
    }
}
