using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace Seed.Infrastructure.Messaging
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _componentContext;
        private readonly ICommandInterceptor[] _interceptors;

        public CommandBus(IComponentContext container)
        {
            _componentContext = container;
            _interceptors = new ICommandInterceptor[0];
        }

        public CommandBus(IComponentContext container, IEnumerable<ICommandInterceptor> interceptors)
        {
            _componentContext = container;
            _interceptors = interceptors.ToArray();
        }

        public virtual async Task<ICommandResult> Submit<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var handler = _componentContext.ResolveOptional<ICommandHandler<TCommand>>();

            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }

            var commandContext = new CommandContext(command);
            
            //await ExecutePreExecuteInterceptors(commandContext);

            //if (!commandContext.AbortExecution)
            //{
                commandContext.Result = await handler.Handle(command);
            //}

            //await ExecutePostExecuteInterceptors(commandContext);

            return commandContext.Result;
        }

        public virtual async Task<ICommandResult<TResult>> Submit<TCommand, TResult>(TCommand command) 
            where TCommand : ICommand<TResult> 
            where TResult : class
        {
            var handler = _componentContext.Resolve<ICommandHandler<TCommand, TResult>>();

            if (handler == null)
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }

            var commandContext = new CommandContext(command);

            await ExecutePreExecuteInterceptors(commandContext);

            if (!commandContext.AbortExecution)
            {
                commandContext.Result = await handler.Execute(command);
            }

            await ExecutePostExecuteInterceptors(commandContext);

            return (ICommandResult<TResult>)commandContext.Result;
        }

        private async Task ExecutePreExecuteInterceptors(ICommandContext commandContext)
        {
            foreach (var interceptor in _interceptors)
            {
                if (!interceptor.ShouldIntercept(commandContext.Command))
                {
                    await interceptor.PreExecute(commandContext);
                }
            }
        }

        private async Task ExecutePostExecuteInterceptors(ICommandContext commandContext)
        {
            foreach (var interceptor in _interceptors)
            {
                if (!interceptor.ShouldIntercept(commandContext.Command))
                {
                    await interceptor.PostExecute(commandContext);
                }
            }
        }

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
