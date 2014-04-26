using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Admin.Users
{
    public class EditUserCommand : ICommand
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}
