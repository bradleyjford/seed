using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Security
{
    public class SignInCommand : ICommand
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
