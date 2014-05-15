using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Messaging;
using Serilog;
using Serilog.Context;

namespace Seed.Api.Infrastructure.Messaging
{
    public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decorated;
        private readonly ILogger _logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> decorated, ILogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public async Task<ICommandResult> Handle(TCommand command)
        {
            ICommandResult result;

            using (LogContext.PushProperty("Command", typeof(TCommand)))
            {
                _logger.Verbose("Begin");

                try
                {
                    result = await _decorated.Handle(command);

                    _logger.Verbose("End");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Unhandled exception");

                    throw;
                }
            }

            return result;
        }
    }
}