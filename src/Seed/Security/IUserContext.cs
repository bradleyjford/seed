using System;

namespace Seed.Security
{
    public interface IUserContext
    {
        int UserId { get; }

        string DisplayName { get; }

        string UserName { get; }
        string EmailAddress { get; }

        bool IsInRole(string role);
    }
}
