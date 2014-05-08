using System;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Seed.Api.Infrastructure.Messaging;
using Seed.Api.Infrastructure.Security;
using Seed.Data;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;
using Seed.Infrastructure.Security;
using Seed.Security;

namespace Seed.Api
{
    public static class AutofacConfig
    {
        public static IContainer Initialize()
        {
            var domainAssembly = typeof(IUserRepository).Assembly;
            var dataAssembly = typeof(UserRepository).Assembly;
            
            var builder = new ContainerBuilder();

            // WebAPI Services
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

            // Messaging Services
            builder.RegisterType<SeedCommandBus>().As<ICommandBus>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandValidator<>))
                .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}