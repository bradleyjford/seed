using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class DeactivateUserCommand : ICommand
    {
        public DeactivateUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; protected set; }
    }
}
