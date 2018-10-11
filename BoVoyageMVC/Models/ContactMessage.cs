using BoVoyageMVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageMVC.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Display(Name = "M/Mme")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "La civilite doit avoir de 1 à 20 caractères")]		
        public string Title { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Le nom doit avoir de 2 à 30 caractères")]	
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "La prénom doit avoir de 2 à 30 caractères")]		
        public string FisrtName { get; set; }

        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Display(Name = "Téléphone")]       
        [RegularExpression(@"^([0-9])*\s*$")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Le numéro de telephone doit avoir de 3 à 20 caractères")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date envoi")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Column(TypeName ="datetime2")]       
        public DateTime SendDate { get; set; }

        [AllowHtml]
        public string Message { get; set; }

    }
}