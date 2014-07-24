using System;

namespace Seed.Common.CommandHandling
{
    public class CommandResult : ICommandResult
    {
        public static CommandResult Ok = new CommandResult(true, null);
        public static CommandResult Fail = new CommandResult(false, null);

        public static ICommandResult CreateFailureWithException(Exception error)
        {
            return new CommandResult(false, error);
        }

        private CommandResult(bool success, Exception error)
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; private set; }
        public Exception Error { get; private set; }
    }

    public class CommandResult<TResult> : ICommandResult<TResult>
        where TResult : class
    {
        public static CommandResult<TResult> Fail = new CommandResult<TResult>(false);

        private CommandResult(bool success)
        {
            Success = success;
        }

        public CommandResult(Exception error)
        {
            Success = false;
            Error = error;
        }

        public CommandResult(TResult value)
            : this(true, value)
        {
        }

        public CommandResult(bool success, TResult value)
        {
            Success = success;
            Value = value;
        }

        public TResult Value { get; private set; }
        public bool Success { get; private set; }
        public Exception Error { get; private set; }
    }
}
