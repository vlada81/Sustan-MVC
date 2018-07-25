using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sustan.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite Vaše ime i prezime.")]
        [Display(Name = "Ime i prezime")]
        [StringLength(100, ErrorMessage = "Ime i prezime ne mogu biti duži od 100 karaktera!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite email adresu.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Format email adrese nije ispravan, molim Vas pokušajte ponovo.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Molim Vas unesite Vašu poruku.")]
        [Display(Name = "Poruka")]
        [StringLength(1000, ErrorMessage = "Poruka ne može biti duža od 1000 karaktera!")]
        public string Message { get; set; }

        [Display(Name = "Telefon")]
        //[RegularExpression(@"^([0-9]|#|\+|\*)+$", ErrorMessage = "Broj telefona nije ispravno unet, molim Vas pokušajte ponovo.")]
        [StringLength(50, ErrorMessage = "Broj telefona ne može biti duži od 50 karaktera!")]
        public string PhoneNumber { get; set; }
    }
}