using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Models
{
	public class Participant : Personne
	{
        [Display(Name ="Participant")]
		[NotMapped]
		public int UniqueNumber => Id;

        [Display(Name = "Réduction")]
        [NotMapped]
		public float Discount
		{
			get
			{
				if (Age < 12)
					return 0.6f;
				else
					return 1.0f;
			}
		}

        [Display(Name = "Réservation")]
        public int DossierReservationId { get; set; }

		[ForeignKey("DossierReservationId")]
		public DossierReservation DossierReservation { get; set; }
	}
}