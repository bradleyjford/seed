using System;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandResult
    {
        bool Success { get; }
        Exception Error { get; }
    }

    public interface ICommandResult<TResult> : ICommandResult
    {
        TResult Result { get; }
    }
}