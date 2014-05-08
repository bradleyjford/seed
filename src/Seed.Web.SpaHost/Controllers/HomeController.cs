using System;
using System.Web.Mvc;

namespace Seed.Web.SpaHost.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}