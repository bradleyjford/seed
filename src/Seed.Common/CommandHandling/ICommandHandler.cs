using System;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult> 
        where TResult : class
    {
        Task<TResult> Handle(TCommand command);
    }
}
