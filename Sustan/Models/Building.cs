using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sustan.Models
{
    public class Building
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite JIBZ!")]
        [RegularExpression(@"\d{2,4}-\d{2,4}", ErrorMessage = "JIBZ broj mora biti u formi 00-00 do 0000-0000 ")]
        public string JIBZ { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite naziv ulice!")]
        [StringLength(100, ErrorMessage = "Naziv ulice ne moze biti duži od 100 karaktera!")]
        [RegularExpression(@"^[\p{L}][\p{L}\s-]*$", ErrorMessage = "Naziv ulice može sadržati samo slova")]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite broj!")]
        [Range(0, 1000, ErrorMessage = "Broj ne može biti manji od 0!")]
        [Display(Name = "Broj")]
        public int Number { get; set; }

        [StringLength(10, ErrorMessage = "Naziv ulaza ne može biti duži od 10 karaktera!")]
        [Display(Name = "Ulaz")]
        public string Entrance { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite spratnost zgrade!")]
        [Range(1, 100, ErrorMessage = "Spratnost mora biti između 1 i 100!")]
        [Display(Name = "Spratnost")]
        public int NumberOfFloors { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite PIB zgrade!")]
        [RegularExpression(@"\d{9}", ErrorMessage = "PIB mora sadržati 9 cifara!")]
        [Display(Name = "PIB")]
        public double Pib { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite Matični broj zgrade!")]
        [RegularExpression(@"\d{8}", ErrorMessage = "Matični broj mora sadržati 8 cifara!")]
        [Display(Name = "Matični broj")]
        public double BuildingRegistrationNumber { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite broj žiro računa zgrade!")]
        [RegularExpression(@"\d{3}-\d{13}-\d{2}", ErrorMessage = "Broj računa mora biti u formi 000-0000000000000-00")]
        [Display(Name = "Broj računa")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite trenutno stanje žiro računa zgrade!")]
        //[DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Stanje računa")]
        public decimal AccountBalance { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite broj parcele zgrade!")]
        [StringLength(50, ErrorMessage = "Broj parcele ne može biti duži od 50 karaktera!")]
        [Display(Name = "Broj parcele")]
        public string ParcelNumber { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite površinu zgrade u osnovi!")]
        [Range(0.00, 10000000.00, ErrorMessage = "Površina ne može biti manja od 0!")]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Za decimalni zapis koristite tačku.")]
        [Display(Name = "Površina zgrade")]
        public decimal BuildingArea { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite upravnika zgrade!")]
        [StringLength(100, ErrorMessage = "Ime i prezime upravnika zgrade ne može biti duže od 100 karaktera.")]
        [RegularExpression(@"^[\p{L}][\p{L}\s-]*$", ErrorMessage = "Ime i prezime upravnika može sadržati samo slova.")]
        [Display(Name = "Upravnik zgrade")]
        public string BuildingManager { get; set; }

        [MaxLength(1024)]
        [Display(Name = "Slika zgrade")]
        public string ImageUrl { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
    }
}