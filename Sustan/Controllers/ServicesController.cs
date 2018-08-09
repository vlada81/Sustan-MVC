using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sustan.Controllers
{
    [RequireHttps]
    [RoutePrefix("Nase-usluge")]
    public class ServicesController : Controller
    {
        // GET: Services
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Services
        [Route("Upravljanje-zgradama")]
        public ActionResult UpravljanjeZgradama()
        {
            return View();
        }

        // GET: Services
        [Route("Odrzavanje-zgrada")]
        public ActionResult OdrzavanjeZgrada()
        {
            return View();
        }

        // GET: Services
        [Route("Knjizenje-zgrada")]
        public ActionResult KnjizenjeZgrada()
        {
            return View();
        }
    }
}