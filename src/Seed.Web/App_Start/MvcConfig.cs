using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Seed.Web.Infrastructure;

namespace Seed.Web.App_Start
{
    public static class MvcConfig
    {
        public static void Configure()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AppViewEngine());

            AreaRegistration.RegisterAllAreas();

            RegisterFilters(GlobalFilters.Filters);            
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}