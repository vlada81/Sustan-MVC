using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sustan.Models;
using Sustan.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Sustan.Repository;
using System.IO;

namespace Sustan.Controllers
{
    [RequireHttps]
    [Authorize]
    [RoutePrefix("Stanovi")]
    public class ApartmentsController : Controller
    {
        private IApartmentRepository _repository { get; set; }

        public ApartmentsController()
        {
            _repository = new ApartmentRepo(new ApplicationDbContext());
        }

        public ApartmentsController(IApartmentRepository repository)
        {
            _repository = repository;
        }


        // GET: Apartments
        [Authorize(Roles = "Admin")]
        [Route("")]
        public ActionResult Index()
        {
            var apartments = _repository.GetAll();
            return View(apartments.ToList());
        }

        // GET: Apartments/List
        [Route("Podaci")]
        public ActionResult List()
        {
            var currentUser = User.Identity.GetUserId();
            var apartment = _repository.GetViewModel(currentUser);
            return View(apartment);
        }


        // GET: Apartments/Details/5
        [Authorize(Roles = "Admin")]
        [Route("Detalji/{id?}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = _repository.GetById(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }


        // GET: Apartments/Create
        [Authorize(Roles = "Admin")]
        [Route("Kreiranje")]
        public ActionResult Create()
        {
            ViewBag.BuildingId = new SelectList(_repository.GetBuildings(), "Id", "JIBZ");
            ViewBag.UserId = new SelectList(_repository.GetUsers(), "Id", "Email");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [Route("Kreiranje")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JIBS,ApartmentNumber,ApartmentArea,NumberOfTenants,CostOfService,BuildingId,UserId,ApartmentBillUrl")] Apartment apartment, HttpPostedFileBase upload)
        {
            if (apartment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Problem postoji kada se kreira novi stan koji ne postoji u bazi jer vraca null vrednost zbog FirstOrDefault()
            //var apartmentNumberInUse = _repository.GetAll().Where(c => c.JIBS == apartment.JIBS);

            //if (apartmentNumberInUse.Last().JIBS == apartment.JIBS)
            //{
            //    ViewBag.Error = "Broj stana te zgrade već postoji u bazi.";

            //    ViewBag.BuildingId = new SelectList(_repository.GetBuildings(), "Id", "JIBZ", apartment.BuildingId);
            //    ViewBag.UserId = new SelectList(_repository.GetUsers(), "Id", "Email", apartment.UserId);

            //    return View(apartment);
            //}

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Uploads/Documents/"), Path.GetFileName(upload.FileName));
                    upload.SaveAs(path);
                    apartment.ApartmentBillUrl = "~/Uploads/Documents/" + upload.FileName;
                }

                _repository.Create(apartment);
                return RedirectToAction("Index");
            }

            ViewBag.BuildingId = new SelectList(_repository.GetBuildings(), "Id", "JIBZ", apartment.BuildingId);
            ViewBag.UserId = new SelectList(_repository.GetUsers(), "Id", "Email", apartment.UserId);
            return View(apartment);
        }


        // GET: Apartments/Edit/5
        [Authorize(Roles = "Admin")]
        [Route("Izmena/{id?}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = _repository.GetById(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(_repository.GetBuildings(), "Id", "JIBZ", apartment.BuildingId);
            ViewBag.UserId = new SelectList(_repository.GetUsers(), "Id", "Email", apartment.UserId);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [Route("Izmena/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JIBS,ApartmentNumber,ApartmentArea,NumberOfTenants,CostOfService,BuildingId,UserId,ApartmentBillUrl")] Apartment apartment, int id, HttpPostedFileBase upload)
        {
            if (id != apartment.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var model = _repository.GetById(id);
                var oldFilePath = model.ApartmentBillUrl;

                if (upload != null && upload.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Uploads/Documents/"), Path.GetFileName(upload.FileName));
                    upload.SaveAs(path);
                    model.ApartmentBillUrl   = "~/Uploads/Documents/" + upload.FileName;
                    string fullPath = Request.MapPath(oldFilePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                _repository.Update(model);

                try
                {
                    _repository.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    if (!_repository.Exists(id))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(apartment);
        }


        // GET: Apartments/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("Brisanje/{id?}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Apartment apartment = _repository.GetById(id);

            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }


        // POST: Apartments/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("Brisanje/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apartment apartment = _repository.GetById(id);

            string fullPath = Request.MapPath(apartment.ApartmentBillUrl);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _repository.Delete(apartment);
            return RedirectToAction("Index");
        }

    }
}
