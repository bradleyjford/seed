﻿using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Integration.WebApi;
using Microsoft.Practices.ServiceLocation;
using Seed.Api.Infrastructure.Security;
using Seed.Data;
using Seed.Data.Admin;
using Seed.Infrastructure.Auditing;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;
using Seed.Security;

namespace Seed.Api
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<AuditingCommandBus>().As<ICommandBus>()
                .InstancePerApiRequest();

            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            //builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
            //    .InstancePerHttpRequest();

            var domainAssembly = typeof(IUserRepository).Assembly;
            var dataAssembly = typeof(UserRepository).Assembly;

            builder.RegisterType<SeedUserContext>().As<IUserContext>()
                .InstancePerApiRequest();

            builder.RegisterType<SeedDbContext>().As<ISeedDbContext>();

            builder.RegisterType<SeedUnitOfWork>().As<ISeedUnitOfWork>()
                .InstancePerApiRequest();

            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerApiRequest();

            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerApiRequest();

            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerApiRequest();
            
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            config.DependencyResolver = resolver;

            var serviceLocator = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }
    }
}