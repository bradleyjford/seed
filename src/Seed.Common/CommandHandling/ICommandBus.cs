using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public interface ICommandBus
    {
        Task<TResult> Send<TResult>(ICommand<TResult> command)
           where TResult : class;

        Task<IEnumerable<ValidationResult>> Validate<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
