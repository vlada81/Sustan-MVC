using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sustan.Controllers
{
    [RequireHttps]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Error/NotFound
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = 500;

            return View();
        }
    }
}