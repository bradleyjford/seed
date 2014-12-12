using System;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Seed.Web.Infrastructure;

namespace Seed.Web.App_Start
{
    public static class MvcConfig
    {
        public static void Configure(IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AppViewEngine());

            RegisterFilters(GlobalFilters.Filters);            
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
        }

        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}