using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Security
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand, bool>
    {
        public bool Execute(SignInCommand command)
        {
            return false;
        }
    }
}
