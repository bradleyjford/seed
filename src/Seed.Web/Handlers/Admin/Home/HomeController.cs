using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seed.Web.Handlers.Admin
{
    [RouteArea(Areas.Admin)]
    [Route("{ action = Index }")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("manage-users")]
        public ActionResult ManageUsers()
        {
            return View();
        }
    }
}