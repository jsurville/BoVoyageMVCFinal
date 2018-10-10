using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Models
{
    public class Image
    {

        public int Id { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string ContentType { get; set;}

        [Required(ErrorMessage = "Vous devez choisir une image.")]
        public byte[] Content { get; set; } 

        [Display(Name= "Destination")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        public int DestinationId { get; set; }

        [ForeignKey("DestinationId")]
        public Destination Destination { get; set; }
    }
}