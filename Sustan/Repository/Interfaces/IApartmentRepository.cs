using Sustan.Models;
using Sustan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sustan.Repository.Interfaces
{
    public interface IApartmentRepository
    {
        IQueryable<Apartment> GetAll();
        Apartment GetById(int? id);
        void Create(Apartment apartment);
        void Update(Apartment apartment);
        void Delete(Apartment apartment);
        void Save();
        bool Exists(int? id);

        IQueryable<Building> GetBuildings();
        IQueryable<ApplicationUser> GetUsers();

        ApartmentBuildingPdfFileViewModel GetViewModel(string id);
    }
}
