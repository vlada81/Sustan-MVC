using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sustan.Controllers
{
    [RoutePrefix("Obrasci")]
    public class DownloadFormsController : Controller
    {
        // GET: DownloadForms
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}