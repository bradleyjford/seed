using System;
using System.Threading.Tasks;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<ICommandResult> Execute(TCommand command);
    }
}
