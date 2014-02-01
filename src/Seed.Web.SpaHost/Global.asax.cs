using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Seed.Web.SpaHost
{
    public class Global : HttpApplication
    {
        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Default",
                "{*catch-all}",
                new { controller = "Home", action = "Index" }
                );
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            MvcHandler.DisableMvcResponseHeader = true;

            RegisterRoutes(RouteTable.Routes);
        }
    }
}