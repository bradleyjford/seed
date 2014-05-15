using System;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommand
    {
        
    }

    public interface ICommand<TResult> : ICommand
    {
    }
}
