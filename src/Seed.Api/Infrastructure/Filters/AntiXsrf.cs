using System;

namespace Seed.Api.Infrastructure.Filters
{
    internal static class AntiXsrf
    {
        public const string CookieName = "__RequestVerificationToken";
    }
}