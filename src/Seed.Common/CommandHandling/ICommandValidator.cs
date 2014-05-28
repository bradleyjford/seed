using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seed.Common.CommandHandling
{
    public interface ICommandValidator<in TCommand> 
        where TCommand : ICommand
    {
        IEnumerable<ValidationResult> Validate(TCommand command);
    }
}
