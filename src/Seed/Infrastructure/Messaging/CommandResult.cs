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

        protected CommandResult(bool success, Exception error)
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; private set; }
        public Exception Error { get; private set; }
    }

    public class CommandResult<TResult> : CommandResult, ICommandResult<TResult>
    {
        public static ICommandResult<TResult> CreateFailureWithException<TResult>(Exception error)
        {
            return new CommandResult<TResult>(error);
        }

        private readonly TResult _result;

        protected CommandResult(Exception error) 
            : base(false, error)
        {
        }

        public CommandResult(bool success, TResult result) 
            : base(success, null)
        {
            _result = result;
        }

        public TResult Result
        {
            get { return _result; }
        }
    }
}
