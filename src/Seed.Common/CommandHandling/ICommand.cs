using System;

namespace Seed.Common.CommandHandling
{
    public interface ICommand
    {
        
    }

    public interface ICommand<out TResult> : ICommand
    {
    }
}
