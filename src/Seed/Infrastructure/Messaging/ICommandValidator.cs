using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seed.Infrastructure.Messaging
{
    public interface ICommandValidator<in TCommand> 
        where TCommand : ICommand
    {
        IEnumerable<ValidationResult> Validate(TCommand command);
    }
}
