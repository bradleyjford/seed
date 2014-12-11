using System;
using System.Web.Mvc;

namespace Seed.Web.Infrastructure
{
    public class AppViewEngine : RazorViewEngine
    {
        public AppViewEngine()
        {
            //{2} is the area name
            //{1} is the controller name
            //{0} is the action name

            AreaViewLocationFormats = new[] {
                "~/Handlers/{2}/{1}/{0}.cshtml",
                "~/Handlers/{2}/SharedViews/{0}.cshtml",
                "~/Handlers/SharedViews/{0}.cshtml"
            };

            AreaMasterLocationFormats = new[] {
                "~/Handlers/{2}/{1}/{0}.cshtml",
                "~/Handlers/{2}/SharedViews/{0}.cshtml",
                "~/Handlers/SharedViews/{0}.cshtml"
            };

            AreaPartialViewLocationFormats = new[] {
                "~/Handlers/{2}/{1}/{0}.cshtml",
                "~/Handlers/{2}/SharedViews/{0}.cshtml",
                "~/Handlers/SharedViews/{0}.cshtml"
            };

            ViewLocationFormats = new[] {
                "~/Handlers/_Default/{1}/{0}.cshtml",
                "~/Handlers/SharedViews/{0}.cshtml"
            };

            MasterLocationFormats = new[] {
                "~/Handlers/_Default/{1}/{0}.cshtml",
                "~/Handlers/SharedViews/{0}.cshtml"
            };

            PartialViewLocationFormats = new[] {
                "~/Handlers/_Default/{1}/{0}.cshtml",
                "~/Handlers/SharedViews/{0}.cshtml"
            };
        }
    }
}
