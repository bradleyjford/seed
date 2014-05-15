using System;
using System.Threading.Tasks;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandHandler<in TCommand>
    {
        Task<ICommandResult> Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult> 
        where TResult : class
    {
        Task<ICommandResult<TResult>> Execute(TCommand command);
    }
}
