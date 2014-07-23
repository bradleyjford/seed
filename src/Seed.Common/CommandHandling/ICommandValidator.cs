using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public interface ICommandValidator<in TCommand> 
        where TCommand : ICommand
    {
        Task<IEnumerable<ValidationResult>> Validate(TCommand command);
    }
}
