using System;

namespace Seed.Security
{
    public interface IUserContext<out TUserId>
    {
        TUserId UserId { get; }

        string DisplayName { get; }

        string UserName { get; }
        string Email { get; }

        bool IsInRole(string role);
    }
}
