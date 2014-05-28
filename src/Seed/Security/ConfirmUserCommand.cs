using System;
using Seed.Common.CommandHandling;

namespace Seed.Security
{
    public class ConfirmUserCommand : ICommand
    {
        public int UserId { get; set; }
    }
}
