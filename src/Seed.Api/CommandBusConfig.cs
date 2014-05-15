using System;
using Seed.Infrastructure.Messaging;

namespace Seed.Api
{
    public static class CommandBusConfig
    {
        public static ICommandBusBuilder Register()
        {
            return new CommandBusBuilder();
            //.Use<UnitOfWorkCommandBusInterceptor>()
            //.Use<AuditCommandBusInterceptor>();
        }
    }
}