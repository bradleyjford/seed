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

        private class CommandHandlerWrapper<TCommand, TResult> : ICommandHandlerWrapper<TResult> 
            where TCommand : ICommand<TResult>
            where TResult : class
        {
            private readonly ICommandHandler<TCommand, TResult> _decorated;

            public CommandHandlerWrapper(ICommandHandler<TCommand, TResult> decorated)
            {
                _decorated = decorated;
            }

            public Task<TResult> Handle(ICommand<TResult> command)
            {
                var typedCommand = (TCommand)command;

                return _decorated.Handle(typedCommand);
            }
        }
    }
}
