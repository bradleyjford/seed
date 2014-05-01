using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Security
{
    public class SignInCommand : ICommand
    {
        public SignInCommand()
        {
        }


        public SignInCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
