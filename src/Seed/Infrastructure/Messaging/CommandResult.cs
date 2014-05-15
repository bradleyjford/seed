using System;

namespace Seed.Infrastructure.Messaging
{
    public class CommandResult : ICommandResult
    {
        public static ICommandResult Ok = new CommandResult(true, null);
        public static ICommandResult Fail = new CommandResult(false, null);

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
        public static ICommandResult<TResult> Fail = new CommandResult<TResult>(false);

        public static ICommandResult<TResult> CreateFailureWithException(Exception error) 
        {
            return new CommandResult<TResult>(error);
        }

        private CommandResult(bool success)
        {
            Success = success;
        }

        protected CommandResult(Exception error)
        {
            Success = false;
            Error = error;
        }

        public CommandResult(TResult value)
        {
            Success = true;
            Value = value;
        }

        public TResult Value { get; private set; }
        public bool Success { get; private set; }
        public Exception Error { get; private set; }
    }
}
