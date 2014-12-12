using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Seed.Web.App_Start;
using Seed.Web.Infrastructure;

namespace Seed.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = AutofacConfig.Initialize();

            MvcConfig.Configure(container);
            
            GlobalConfiguration.Configure(WebApiConfig.Configure);
        }
    }
}
