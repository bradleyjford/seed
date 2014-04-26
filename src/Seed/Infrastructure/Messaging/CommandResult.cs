using System;

namespace Seed.Infrastructure.Messaging
{
    public class CommandResult : ICommandResult
    {
        public static CommandResult Ok = new CommandResult(true, null);
        public static CommandResult Fail = new CommandResult(false, null);

        private CommandResult(bool success, Exception error)
        {
            Success = success;
            Error = error;
        }

        public CommandResult(Exception error)
            : this(false, error)
        {;
        }

        public bool Success { get; private set; }

        public Exception Error { get; private set; }
    }
}
