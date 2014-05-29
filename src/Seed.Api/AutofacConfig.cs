using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Seed.Api.Infrastructure.Security;
using Seed.Common.CommandHandling;
using Seed.Common.Security;
using Seed.Data;
using Seed.Infrastructure.CommandHandlerDecorators;
using Seed.Lookups;
using Seed.Security;

namespace Seed.Api
{
    public static class AutofacConfig
    {
        public static IContainer Initialize()
        {
            var domainAssembly = typeof(LookupEntity).Assembly;
            var dataAssembly = typeof(SeedDbContext).Assembly;

            var builder = new ContainerBuilder();

            // WebAPI Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .InstancePerLifetimeScope();

            // Domain Services
            builder.RegisterType<SeedUserContext>().As<IUserContext>()
                .InstancePerLifetimeScope();

            // Data Services
            builder.RegisterType<SeedDbContext>().As<ISeedDbContext>()
                .InstancePerLifetimeScope();

            // Security Services
            builder.RegisterType<RandomNumberGenerator>().As<IRandomNumberGenerator>()
                .SingleInstance();

            builder.RegisterType<Rfc2898PasswordHasher>()
                .WithParameter("computeParameters", Rfc2898PasswordHashParameters.Version0)
                .WithParameter("validationParameters", Rfc2898PasswordHashParameters.AllVersions)
                .As<IPasswordHasher>()
                .SingleInstance();

            // Commanding
            builder.RegisterType<CommandBus>().As<ICommandBus>()
                .InstancePerLifetimeScope();

            // Command Handlers
            builder.RegisterAssemblyTypes(domainAssembly)
                .As(t => t.GetInterfaces()
                    .Where(i => i.IsClosedTypeOf(typeof(ICommandHandler<,>)))
                        .Select(i => new KeyedService("commandHandler", i)));
               
            // Command Handler Decorators
            builder.RegisterGenericDecorator(
                typeof(AuditCommandHandlerDecorator<,>),
                typeof(ICommandHandler<,>),
                "commandHandler")
                .Keyed("auditDecoratedCommandHandler", typeof(ICommandHandler<,>));

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerDecorator<,>),
                typeof(ICommandHandler<,>),
                "auditDecoratedCommandHandler");

            // Command Validators
            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandValidator<>))
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
