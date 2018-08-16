using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sustan.Models;

namespace Sustan.Repository.Interfaces
{
    public interface IPdfFilePathRepository
    {
        IQueryable<PdfFilePath> GetAll();
        PdfFilePath GetById(int? id);
        void Create(PdfFilePath filePath);
        void Update(PdfFilePath filePath);
        void Delete(PdfFilePath filePath);
        void Save();
        bool Exist(int? id);

        IQueryable<Building> GetBuildings();

    }
}
