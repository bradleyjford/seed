using System;

namespace Seed.Api.Infrastructure
{
    internal static class AntiXsrf
    {
        public const string CookieName = "__RequestVerificationToken";
    }
}