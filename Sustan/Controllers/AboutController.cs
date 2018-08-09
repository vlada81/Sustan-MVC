using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sustan.Controllers
{
    [RequireHttps]
    [RoutePrefix("O-nama")]
    public class AboutController : Controller
    {
        // GET: About
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}