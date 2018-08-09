using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sustan.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [EmailAddress(ErrorMessage = "Email adresa nije uneta u pravilnom formatu")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(50, ErrorMessage = "Ime ne može biti duže od 50 karaktera")]
        [Display(Name = "Ime")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(50, ErrorMessage = "Prezime ne može biti duže od 50 karaktera")]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(100, ErrorMessage = "Lozinka ne može biti duža od 100 karaktera")]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [Display(Name = "Registracija")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy (HH:mm)}")]
        public DateTime RegistrationDate { get; set; }
    }
}