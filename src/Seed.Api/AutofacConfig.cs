using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
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

            builder.RegisterType<CommandBus>().As<ICommandBus>()
                .InstancePerApiRequest();

            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            //builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
            //    .InstancePerHttpRequest();

            var domainAssembly = typeof(IUserRepository).Assembly;  

            builder.RegisterAssemblyTypes(domainAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerApiRequest();

            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerApiRequest();

            builder.RegisterAssemblyTypes(domainAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerApiRequest();
            
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            config.DependencyResolver = resolver;
        }
    }
}