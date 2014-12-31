using System;
using Seed.Common.CommandHandling;

namespace Seed.Security
{
    public class ConfirmUserCommand : ICommand<CommandResult>
    {
        public int UserId { get; set; }
    }
}
