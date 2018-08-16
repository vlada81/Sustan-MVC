using Sustan.Models;
using Sustan.Repository;
using Sustan.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sustan.Controllers
{
    [RequireHttps]
    [Authorize]
    [RoutePrefix("Dokumentacija")]
    public class PdfFilePathsController : Controller
    {
        private IPdfFilePathRepository _repository { get; set; }

        public PdfFilePathsController()
        {
            _repository = new PdfFilePathRepo(new ApplicationDbContext());
        }

        public PdfFilePathsController(IPdfFilePathRepository repository)
        {
            _repository = repository;
        }


        // GET: PdfFilePaths
        [Authorize(Roles = "Admin")]
        [Route("")]
        public ActionResult Index()
        {
            var filePaths = _repository.GetAll();
            return View(filePaths.ToList());
        }


        // GET: PdfFilePaths/Create
        [Authorize(Roles = "Admin")]
        [Route("Dodavanje")]
        public ActionResult Create()
        {
            ViewBag.BuildingId = new SelectList(_repository.GetBuildings(), "Id", "JIBZ");
            return View();
        }

        // POST: PdfFilePaths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [Route("Dodavanje")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, PdfFileName, PdfFileUrl, BuildingId")] PdfFilePath filePath, HttpPostedFileBase[] uploadPdfs)
        {
            if (filePath == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {

                foreach (HttpPostedFileBase item in uploadPdfs)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        filePath.PdfFileName = Path.GetFileName(item.FileName);
                        filePath.PdfFileUrl = Path.Combine(Server.MapPath("~/Uploads/Documents/"), Path.GetFileName(item.FileName));

                        if (System.IO.File.Exists(filePath.PdfFileUrl))
                        {
                            ViewBag.Error = "Fajl sa istim imenom već postoji u bazi!";
                            ViewBag.BuildingId = new SelectList(_repository.GetBuildings(), "Id", "JIBZ");
                            return View(filePath);
                        }
                        else
                        {
                            item.SaveAs(filePath.PdfFileUrl);
                            filePath.PdfFileUrl = "~/Uploads/Documents/" + item.FileName;
                        }
                    }

                    _repository.Create(filePath);
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.BuildingId = new SelectList(_repository.GetBuildings(), "Id", "JIBZ", filePath.BuildingId);
            return View(filePath);
        }

        [Route("Brisanje/{id?}")]
        // GET: PdfFilePaths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PdfFilePath filePath = _repository.GetById(id);
            if (filePath == null)
            {
                return HttpNotFound();
            }
            return View(filePath);
        }

        [Route("Brisanje/{id?}")]
        // POST: PdfFilePaths/Delete/5
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

                PdfFilePath filePath = _repository.GetById(id);

                //Deleting pdf file from server
                string fullPath = Request.MapPath(filePath.PdfFileUrl);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                _repository.Delete(filePath);
            }
            catch (Exception)
            {
                throw;
            }
           
            return RedirectToAction("Index");
        }
    }
}