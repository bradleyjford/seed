using System;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public partial class CommandBus
    {
        private interface ICommandHandlerWrapper<TResult>
            where TResult : class
        {
            Task<TResult> Handle(ICommand<TResult> command);
        }

        private class CommandHandlerWrapper<TMessage, TResponse> : ICommandHandlerWrapper<TResponse> 
            where TMessage : ICommand<TResponse>
            where TResponse : class
        {
            private readonly ICommandHandler<TMessage, TResponse> _decorated;

            public CommandHandlerWrapper(ICommandHandler<TMessage, TResponse> decorated)
            {
                _decorated = decorated;
            }

            public Task<TResponse> Handle(ICommand<TResponse> command)
            {
                var typedCommand = (TMessage)command;

                return _decorated.Handle(typedCommand);
            }
        }
    }
}
