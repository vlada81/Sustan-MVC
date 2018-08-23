using Sustan.Models;
using Sustan.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Sustan.ViewModels;

namespace Sustan.Repository
{
    public class PdfFilePathRepo : IDisposable, IPdfFilePathRepository
    {
        private ApplicationDbContext db;

        public PdfFilePathRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<PdfFilePath> GetAll()
        {
            return db.PdfFilePaths.Include(b => b.Building);
        }

        public IQueryable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        public PdfFilePath GetById(int? id)
        {
            return db.PdfFilePaths.Include(b => b.Building).SingleOrDefault(f => f.Id == id);
        }

        public void Create(PdfFilePath filePath)
        {
            db.PdfFilePaths.Add(filePath);
            db.SaveChanges();
        }

        public void Update(PdfFilePath filePath)
        {
            db.Entry(filePath).State = EntityState.Modified;
        }

        public void Delete(PdfFilePath filePath)
        {
            db.PdfFilePaths.Remove(filePath);
            db.SaveChanges();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public bool Exist(int? id)
        {
            return db.PdfFilePaths.Include(b => b.Building).Count(f => f.Id == id) > 0;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}