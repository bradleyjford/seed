using System;
using System.Threading.Tasks;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        public Task<ICommandResult> Execute(SignInCommand command)
        {
            if (command.Username == "test")
            {
                return Task.FromResult(CommandResult.Ok);
            }

            return Task.FromResult(CommandResult.Fail);
        }
    }
}
