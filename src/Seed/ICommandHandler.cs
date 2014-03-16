using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed
{
    public interface ICommandHandler<in TCommand, out TCommandResult>
        where TCommand : ICommand
    {
        TCommandResult Execute(TCommand command);
    }
}
