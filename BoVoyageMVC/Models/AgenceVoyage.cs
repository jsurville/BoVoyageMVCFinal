using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Models
{
	[Table("AgencesVoyages")]
	public class AgenceVoyage
	{
		public int Id { get; set; }

        [Display(Name="Nom de l'Agence")]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[Index(IsUnique = true)]
		[StringLength(60, MinimumLength = 5, ErrorMessage = "Le nom de l'agence doit avoir de 5 a 60 caracteres")]
		public string Name { get; set; }

		public ICollection<Voyage> Voyages { get; set; }
	}
}