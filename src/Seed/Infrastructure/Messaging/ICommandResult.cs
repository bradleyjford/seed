using System;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandResult
    {
        bool Success { get; }
        Exception Error { get; }
    }

    public interface ICommandResult<out TResult> : ICommandResult
        where TResult : class
    {
        TResult Value { get; }
    }
}