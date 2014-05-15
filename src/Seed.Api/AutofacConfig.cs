﻿using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Seed.Api.Infrastructure.Messaging;
using Seed.Api.Infrastructure.Security;
using Seed.Data;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;
using Seed.Infrastructure.Security;
using Seed.Security;
using Serilog;

namespace Seed.Api
{
    public static class AutofacConfig
    {
        public static IContainer Initialize()
        {
            var domainAssembly = typeof(IUserRepository).Assembly;
            var dataAssembly = typeof(UserRepository).Assembly;

            var builder = new ContainerBuilder();
            
            // Logging
            builder.Register((c, p) => 
                new LoggerConfiguration()
                    .WriteTo.File(@"C:\Temp\Debug.log", 
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {RequestId} {UserId:00000} {Command} {Message}{NewLine}{Exception}")
                    .MinimumLevel.Verbose()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("RequestId", Guid.NewGuid())
                    .Enrich.WithProperty("UserId", c.IsRegistered<IUserContext>() ? (int?)c.Resolve<IUserContext>().UserId : null)
                    .CreateLogger())
                .InstancePerLifetimeScope();

            // WebAPI Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .InstancePerLifetimeScope();

            // Domain Services
            builder.RegisterType<SeedUserContext>().As<IUserContext>()
                .InstancePerLifetimeScope();

            // Data Services
            builder.RegisterType<SeedUnitOfWork>().As<ISeedUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
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
                    .Where(i => i.IsClosedTypeOf(typeof(ICommandHandler<>)))
                        .Select(i => new KeyedService("commandHandler", i)));

            builder.RegisterAssemblyTypes(domainAssembly)
                .As(t => t.GetInterfaces()
                    .Where(i => i.IsClosedTypeOf(typeof(ICommandHandler<,>))));
               
            // Command Handler Decorators
            builder.RegisterGenericDecorator(
                typeof(AuditCommandHandlerDecorator<>),
                typeof(ICommandHandler<>),
                "commandHandler")
                .Keyed("auditDecoratedCommandHandler", typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(UnitOfWorkCommandHandlerDecorator<>),
                typeof(ICommandHandler<>),
                "auditDecoratedCommandHandler")
                .Keyed("unitOfWorkDecoratedCommandHandler", typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(LoggingCommandHandlerDecorator<>),
                typeof(ICommandHandler<>),
                "unitOfWorkDecoratedCommandHandler");

            // Command Validators
            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandValidator<>))
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
