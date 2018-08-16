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

namespace Sustan.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
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
        public ActionResult Index()
        {
            var buildings = _repository.GetAll();
            return View(buildings.ToList());
        }

        [Route("Detalji/{id?}")]
        // GET: Buildings/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Kreiranje")]
        [HttpPost]
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

                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Uploads/Images/"), Path.GetFileName(uploadImage.FileName));

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

                foreach (HttpPostedFileBase item in uploadPdf)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        var pdfFile = new PdfFilePath
                        {
                            PdfFileName = Path.GetFileName(item.FileName),
                            PdfFileUrl = Path.Combine(Server.MapPath("~/Uploads/Documents/"), Path.GetFileName(item.FileName))
                        };

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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JIBZ,Street,Number,Entrance,NumberOfFloors,Pib,BuildingRegistrationNumber,AccountNumber,AccountBalance,ParcelNumber,BuildingArea,BuildingManager,ImageUrl")] Building building, int id, HttpPostedFileBase uploadImage)
        {
            if (id != building.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var model = _repository.GetById(building.Id);
                var oldImagePath = model.ImageUrl;

                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Uploads/Images/"), Path.GetFileName(uploadImage.FileName));

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ErrorMessage = "Fajl sa istim imenom već postoji u bazi!";
                        return View(building);
                    }
                    else
                    {
                        uploadImage.SaveAs(path);

                        model.ImageUrl = "~/Uploads/Images/" + uploadImage.FileName;
                        string fullPath = Request.MapPath(oldImagePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
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

            return View(building);
        }

        [Route("Brisanje/{id?}")]
        // GET: Buildings/Delete/5
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
                    if (apartment.BuildingId == building.Id)
                    {
                        ViewBag.MessageTitle = "Nije moguće obrisati zgradu!";
                        ViewBag.Message = "Prvo morate obrisati sve stanove koji pripadaju zgradi.";
                        return View(building);
                    }
                }

                //Deleting image file from server
                string fullPath = Request.MapPath(building.ImageUrl);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
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
