using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        public ICommandResult Execute(SignInCommand command)
        {
            if (command.Username == "test")
            {
                return CommandResult.Ok;
            }

            return CommandResult.Fail;
        }
    }
}
