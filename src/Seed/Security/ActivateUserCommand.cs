using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class ActivateUserCommand : ICommand
    {
        public ActivateUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; protected set; }
    }
}
