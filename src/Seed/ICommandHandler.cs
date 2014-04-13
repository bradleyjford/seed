using System;

namespace Seed
{
    public interface ICommandHandler<in TCommand, out TCommandResult>
        where TCommand : ICommand
    {
        TCommandResult Execute(TCommand command);
    }
}
