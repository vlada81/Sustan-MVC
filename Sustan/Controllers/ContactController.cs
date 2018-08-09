using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Sustan.Models;
using System.Threading.Tasks;

namespace Sustan.Controllers
{
    [RequireHttps]
    [RoutePrefix("Kontakt")]
    public class ContactController : Controller
    {
        // GET: Contact
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Contact
        [Route("")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Id,FullName,Email,PhoneNumber,Message")] Contact form)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>-- Ova poruka je poslata preko kontakt forme sa sajta Sustan (https://sustan.rs) --</p><br /><p><strong>Od:</strong> {0} ({1})</p><p><strong>Broj telefona:</strong> {2}</p><p><strong>Poruka:</strong></p><p>{3}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("prijave@sustan.rs"));
                message.Subject = "[Kontakt forma] - Sustan";
                message.Body = string.Format(body, form.FullName, form.Email, form.PhoneNumber, form.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Index");
                }
            }

            return View(form);
        }
    }
}