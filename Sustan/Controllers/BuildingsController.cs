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
using Sustan.Repository;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Sustan.Controllers
{
    [RequireHttps]
    [Authorize]
    [RoutePrefix("Zgrade")]
    public class BuildingsController : Controller
    {
        private IBuildingRepository _repository { get; set; }

        public BuildingsController()
        {
            _repository = new BuildingRepo(new ApplicationDbContext());
        }

        public BuildingsController(IBuildingRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        // GET: Buildings
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var buildings = _repository.GetAll();
            return View(buildings.ToList());
        }

        [Route("Detalji/{id?}")]
        // GET: Buildings/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = _repository.GetById(id);
                        
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        [Route("Kreiranje")]
        // GET: Buildings/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Kreiranje")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JIBZ,Street,Number,Entrance,NumberOfFloors,Pib,BuildingRegistrationNumber,AccountNumber,AccountBalance,ParcelNumber,BuildingArea,BuildingManager,ImageUrl")] Building building, HttpPostedFileBase uploadImage, HttpPostedFileBase[] uploadPdf)
        {
            if (building == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Sve zgrade u bazi
            var existingBuilding = _repository.GetAll();

            if (ModelState.IsValid)
            {
                //Provera da li postoji JIBZ zgrade u bazi
                foreach (var item in existingBuilding)
                {
                    if (item.JIBZ == building.JIBZ)
                    {
                        ViewBag.Error = "JIBZ zgrade već postoji u bazi.";

                        return View(building);
                    }
                }

                // Dodavanje slike prilikom kreiranja zgrade
                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Uploads/Images/"), Path.GetFileName(uploadImage.FileName));

                    // Provarava da li slika sa istim imenom vec postoji u bazi
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ErrorMessage = "Fajl sa istim imenom već postoji u bazi!";
                        return View(building);
                    }
                    else
                    {
                        uploadImage.SaveAs(path);
                        building.ImageUrl = "~/Uploads/Images/" + uploadImage.FileName;
                    }
                    
                }

                building.PdfFilePaths = new List<PdfFilePath>();

                // Dodavanje pdf fajlova dokumentacije prilikom kreiranja zgrade
                foreach (HttpPostedFileBase item in uploadPdf)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        var pdfFile = new PdfFilePath
                        {
                            PdfFileName = Path.GetFileName(item.FileName),
                            PdfFileUrl = Path.Combine(Server.MapPath("~/Uploads/Documents/"), Path.GetFileName(item.FileName))
                        };

                        // Provarava da li pdf fajl sa istim imenom vec postoji u bazi
                        if (System.IO.File.Exists(pdfFile.PdfFileUrl))
                        {
                            ViewBag.ErrorMessage = "Fajl sa istim imenom već postoji u bazi!";
                            return View(building);
                        }
                        else
                        {
                            item.SaveAs(pdfFile.PdfFileUrl);
                            pdfFile.PdfFileUrl = "~/Uploads/Documents/" + item.FileName;

                            building.PdfFilePaths.Add(pdfFile);
                        }
                    }
                }

                _repository.Create(building);
                return RedirectToAction("Index");
            }

            return View(building);
        }

        [Route("Izmena/{id?}")]
        [Authorize(Roles = "Admin")]
        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = _repository.GetById(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Izmena/{id?}")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JIBZ,Street,Number,Entrance,NumberOfFloors,Pib,BuildingRegistrationNumber,AccountNumber,AccountBalance,ParcelNumber,BuildingArea,BuildingManager,ImageUrl")] Building building, int id, HttpPostedFileBase uploadImage)
        {
            if (id != building.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var oldImagePath = building.ImageUrl;

                // Izmena slike zgrade
                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Uploads/Images/"), Path.GetFileName(uploadImage.FileName));

                    // Provarava da li slika sa istim imenom vec postoji u bazi
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ErrorMessage = "Fajl sa istim imenom već postoji u bazi!";
                        return View(building);
                    }
                    else
                    {
                        uploadImage.SaveAs(path);

                        building.ImageUrl = "~/Uploads/Images/" + uploadImage.FileName;
                        string fullPath = Request.MapPath(oldImagePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }

                _repository.Update(building);

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

            return View(building);
        }

        [Route("Brisanje/{id?}")]
        // GET: Buildings/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Building building = _repository.GetById(id);

            if (building == null)
            {
                return HttpNotFound();
            }

            return View(building);
        }

        [Route("Brisanje/{id?}")]
        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Building building = _repository.GetById(id);

                var apartments = _repository.GetApartments();
                foreach (var apartment in apartments)
                {
                    // Proverava da li zgrada ima kreirane stanove 
                    if (apartment.BuildingId == building.Id)
                    {
                        ViewBag.MessageTitle = "Nije moguće obrisati zgradu!";
                        ViewBag.Message = "Prvo morate obrisati sve stanove koji pripadaju zgradi.";
                        return View(building);
                    }
                }

                // Brisanje slike zgrade sa servera
                string fullPath = Request.MapPath(building.ImageUrl);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                // Brisanje pdf fajlova sa servera
                foreach (var item in building.PdfFilePaths)
                {
                    string filePath = Request.MapPath(item.PdfFileUrl);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _repository.Delete(building);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }
      
    }
}
