using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        ICommandResult Submit<TCommand>(TCommand command) 
            where TCommand : ICommand;
        
        IEnumerable<ValidationResult> Validate<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
