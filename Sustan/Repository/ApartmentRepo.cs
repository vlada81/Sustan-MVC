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
    public class ApartmentRepo : IDisposable, IApartmentRepository
    {
        private ApplicationDbContext db;

        public ApartmentRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<Apartment> GetAll()
        {
            return db.Apartments.Include(a => a.Building).Include(a => a.User);
        }

        public Apartment GetById(int? id)
        {
            return db.Apartments.Include(a => a.Building).SingleOrDefault(a => a.Id == id);
        }

        public ApartmentBuildingViewModel GetViewModel(string id)
        {
            ApartmentBuildingViewModel apartment = new ApartmentBuildingViewModel();
            apartment.Apartments = db.Apartments.Include(u => u.User).Where(a => a.UserId == id).Include(a => a.Building);

            return apartment;
        }

        public void Create(Apartment apartment)
        {
            db.Apartments.Add(apartment);
            db.SaveChanges();
        }

        public void Update(Apartment apartment)
        {
            db.Entry(apartment).State = EntityState.Modified;
        }

        public void Delete(Apartment apartment)
        {
            db.Apartments.Remove(apartment);
            db.SaveChanges();
        }

        public bool Exists(int? id)
        {
            return db.Apartments.Include(a => a.Building).Include(a => a.User).Count(a => a.Id == id) > 0;
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

        public IQueryable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return db.Users;
        }
    }
}