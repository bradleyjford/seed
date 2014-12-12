using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Seed.Admin.Lookups;
using Seed.Common.CommandHandling;
using Seed.Common.CommandHandling.Decorators;
using Seed.Common.Domain;
using Seed.Common.Security;
using Seed.Infrastructure.CommandHandlerDecorators;
using Seed.Infrastructure.Data;
using Seed.Lookups;
using Seed.Security;
using Seed.Web.Infrastructure.Security;

namespace Seed.Web
{
    public static class AutofacConfig
    {
        public static IContainer Initialize()
        {
            var domainAssembly = typeof(LookupEntity).Assembly;
            //var dataAssembly = typeof(SeedDbContext).Assembly;

            var builder = new ContainerBuilder();

            RegisterMvcComponents(builder);

            // Domain Services
            builder.RegisterType<SeedUserContext>().As<IUserContext<Guid>>()
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

            RegisterLookupCommandHandlers(builder, domainAssembly);

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

        private static void RegisterMvcComponents(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // Web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // Property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // Property injection into action filters.
            builder.RegisterFilterProvider();
        }

        private static void RegisterLookupCommandHandlers(ContainerBuilder builder, Assembly domainAssembly)
        {
            var lookupEntityTypes = domainAssembly.GetTypes()
                .Where(t => typeof(ILookupEntity).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var lookupEntityType in lookupEntityTypes)
            {
                var createCommandType = typeof(CreateLookupCommand<>).MakeGenericType(lookupEntityType);
                var editCommandType = typeof(EditLookupCommand<>).MakeGenericType(lookupEntityType);
                var activateCommandType = typeof(ActivateLookupCommand<>).MakeGenericType(lookupEntityType);
                var deactivateCommandType = typeof(DeactivateLookupCommand<>).MakeGenericType(lookupEntityType);

                RegisterLookupCommandHandler(builder, createCommandType, lookupEntityType);
                RegisterLookupCommandHandler(builder, editCommandType, lookupEntityType);
                RegisterLookupCommandHandler(builder, activateCommandType, lookupEntityType);
                RegisterLookupCommandHandler(builder, deactivateCommandType, lookupEntityType);
            }
        }

        private static void RegisterLookupCommandHandler(
            ContainerBuilder builder, 
            Type commandType, 
            Type lookupEntityType)
        {
            var handlerType = typeof(LookupEntityCommandHandlers<>).MakeGenericType(lookupEntityType);
            var serviceType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(CommandResult));

            builder.RegisterType(handlerType).Keyed("commandHandler", serviceType);
        }
    }
}
