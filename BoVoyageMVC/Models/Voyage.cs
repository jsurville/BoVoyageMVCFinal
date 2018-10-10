using BoVoyDateMVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Models
{
	public class Voyage
	{
		public int Id { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name ="Date Aller")]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[Index("IX_DatesAgenceDestination", 1, IsUnique = true)]
		[Date(1,15, ErrorMessage = "La Date de départ doit se situer entre demain et dans 15 jours")]
		public DateTime DepartureDate { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Date Retour")]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[Index("IX_DatesAgenceDestination", 2, IsUnique = true)]
		[Date(2, ErrorMessage ="La Date de retour est invalide")]
		public DateTime ReturnDate { get; set; }

		[Display(Name = "Places Disponibles")]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		public int MaxCapacity { get; set; }

		[Range(0, 50000)]
		[Display(Name = "Prix/pers")]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		public double UnitPrice { get; set; }

		[Range(1, 3)]
		[Display(Name = "Marge")]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		public double Margin { get; set; }

		[Display(Name ="Prix/client")]
		[NotMapped]
		public double UnitPublicPrice
		{
			get
			{
				return UnitPrice * Margin;
			}
		}

		[Display(Name ="Agence Voyage")]
		[Index("IX_DatesAgenceDestination", 3, IsUnique = true)]
		public int AgenceVoyageId { get; set; }

		[ForeignKey("AgenceVoyageId")]
		public AgenceVoyage AgenceVoyage { get; set; }

		[Display(Name = "Destination")]
		[Index("IX_DatesAgenceDestination", 4, IsUnique = true)]
		public int DestinationId { get; set; }

		[ForeignKey("DestinationId")]
		public Destination Destination { get; set; }

		public ICollection<DossierReservation> DossiersReservations { get; set; }

	}
}