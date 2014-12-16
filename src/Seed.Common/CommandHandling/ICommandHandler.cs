using System;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public interface ICommandHandler<in TMessage, TResponse>
        where TMessage : ICommand<TResponse> 
        where TResponse : class
    {
        Task<TResponse> Handle(TMessage command);
    }
}
