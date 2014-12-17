using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public interface ICommandBus
    {
        Task<TResult> Execute<TResult>(ICommand<TResult> command)
           where TResult : class;

        Task<IEnumerable<ValidationResult>> Validate<TCommand>(TCommand command)
            where TCommand : ICommand;

        //Task<TResult> Execute<TResult>(IQuery<TResult> query)
        //    where TResult : class;
    }
}
