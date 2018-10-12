using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageMVC.Models
{
	public class Destination
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[Index("IX_ContinentPaysRegion", 1, IsUnique = true)]
		[StringLength(40, MinimumLength = 3, ErrorMessage = "Le nom du continent doit avoir de 3 a 40 caracteres")]
		public string Continent { get; set; }

		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Display(Name = "Pays")]
        [Index("IX_ContinentPaysRegion", 2, IsUnique = true)]
		[StringLength(40, MinimumLength = 3, ErrorMessage = "Le nom du pays doit avoir de 3 a 40 caracteres")]
		public string Country { get; set; }

        
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[Index("IX_ContinentPaysRegion", 3, IsUnique = true)]
		[StringLength(40, MinimumLength = 3, ErrorMessage = "Le nom de la region doit avoir de 3 a 40 caracteres")]
		public string Region { get; set; }

        [AllowHtml]
		public string Description { get; set; }

		public ICollection<Voyage> Voyages { get; set; }

        [Display(Name = "Images")]
        public ICollection<Image> Images { get; set; }

        [NotMapped]
        [Display(Name = "nom")]
        public string Name
        {
            get
            {
                return this.Country+" "+this.Region;
            }
        }
    }   
}