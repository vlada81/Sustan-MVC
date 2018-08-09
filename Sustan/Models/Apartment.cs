using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Sustan.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite JIBS!")]
        [RegularExpression(@"\d{2,4}-\d{2,4}-\d{2,4}", ErrorMessage = "JIBS broj mora biti u formi 00-00-00 do 0000-0000-0000")]
        public string JIBS { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite broj stana!")]
        [Range(0, 200, ErrorMessage = "Broj stana mora biti između 0 i 200!")]
        [Display(Name = "Broj stana")]
        public int ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite kvadraturu stana!")]
        [Range(0.00, 1000.00, ErrorMessage = "Kvadratura stana ne može biti manja od 0!")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Za decimalni zapis koristite tačku.")]
        [Display(Name = "Kvadratura stana")]
        public decimal ApartmentArea { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite broj članova domaćinstva!")]
        [Range(1, 10, ErrorMessage = "Broj članova ne može biti manji od 1 niti veći od 10!")]
        [Display(Name = "Broj članova")]
        public int NumberOfTenants { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite etažninu!")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Za decimalni zapis koristite tačku.")]
        //[DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Etažnina")]
        public decimal CostOfService { get; set; }



        [Required(ErrorMessage = "Molim Vas izberite zgradu!")]
        [Display(Name = "JIBZ")]
        public int BuildingId { get; set; }
        public Building Building { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}