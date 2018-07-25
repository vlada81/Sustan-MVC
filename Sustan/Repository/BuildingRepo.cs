using Sustan.Models;
using Sustan.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sustan.Repository
{
    public class BuildingRepo : IDisposable, IBuildingRepository
    {
        private ApplicationDbContext db;

        public BuildingRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Building> GetAll()
        {
            return db.Buildings;
        }

        public Building GetById(int? id)
        {
            return db.Buildings.SingleOrDefault(b => b.Id == id);
        }

        public void Create(Building building)
        {
            db.Buildings.Add(building);
            db.SaveChanges();
        }

        public void Update(Building building)
        {
            db.Entry(building).State = EntityState.Modified;
        }

        public void Delete(Building building)
        {
            db.Buildings.Remove(building);
            db.SaveChanges();
        }
        
        public bool Exists(int? id)
        {
            return db.Buildings.Count(b => b.Id == id) > 0;
        }

        public void Save()
        {
            db.SaveChanges();
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

        public IQueryable<Apartment> GetApartments()
        {
            return db.Apartments.Include(a => a.Building).Include(u => u.User);
        }
    }
}