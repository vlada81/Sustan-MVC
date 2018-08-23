using Sustan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sustan.ViewModels
{
    public class ApartmentBuildingPdfFileViewModel
    {
        public IEnumerable<Apartment> Apartments { get; set; }
        public Apartment Apartment { get; set; }
        public IEnumerable<PdfFilePath> PdfFilePaths { get; set; }
    }
}