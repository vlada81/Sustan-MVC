using Sustan.Models;
using Sustan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sustan.Repository.Interfaces
{
    public interface IBuildingRepository
    {
        IQueryable<Building> GetAll();
        Building GetById(int? id);
        void Create(Building building);
        void Update(Building building);
        void Delete(Building building);
        void Save();
        bool Exists(int? id);

        IQueryable<Apartment> GetApartments();
    }
}
