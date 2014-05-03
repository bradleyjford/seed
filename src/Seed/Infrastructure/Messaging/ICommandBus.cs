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
        
        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
