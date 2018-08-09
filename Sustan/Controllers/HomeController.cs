using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sustan.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("zasto-da-angazujemo-profesionalnog-upravnika")]
        public ActionResult Info()
        {
            return View();
        }
    }
}