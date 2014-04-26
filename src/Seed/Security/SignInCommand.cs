using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class SignInCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
