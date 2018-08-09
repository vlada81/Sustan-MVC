using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sustan.Controllers
{
    [RequireHttps]
    [RoutePrefix("Cesta-pitanja")]
    public class FaqController : Controller
    {
        // GET: Faq
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}