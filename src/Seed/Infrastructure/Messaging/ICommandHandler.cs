using System;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        ICommandResult Execute(TCommand command);
    }
}
