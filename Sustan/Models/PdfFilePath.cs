using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sustan.Models
{
    public class PdfFilePath
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Naziv datoteke")]
        public string PdfFileName { get; set; }

        [MaxLength(1024)]
        [Display(Name = "Lokacija datoteke")]
        public string PdfFileUrl { get; set; }

        [Required(ErrorMessage = "Molim Vas izberite zgradu!")]
        [Display(Name = "JIBZ")]
        public int BuildingId { get; set; }
        public Building Building { get; set; }

    }
}