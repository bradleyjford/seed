using System;
using System.Web.Mvc;
using Seed.Web.SpaHost.Infrastructure;

namespace Seed.Web.SpaHost.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [ApplyAntiForgeryToken]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}