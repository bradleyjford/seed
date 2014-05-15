using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        Task<ICommandResult> Submit<TCommand>(TCommand command) 
            where TCommand : ICommand;

        Task<ICommandResult<TResult>> Submit<TCommand, TResult>(TCommand command)
           where TCommand : ICommand<TResult> 
           where TResult : class;

        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
