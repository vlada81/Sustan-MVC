using Sustan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sustan.ViewModels
{
    public class ApartmentBuildingViewModel
    {
        public IEnumerable<Apartment> Apartments { get; set; }
        public Apartment Apartment { get; set; }
    }
}