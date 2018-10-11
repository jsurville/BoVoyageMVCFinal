using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Models
{
	public class Client : Personne
	{
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<DossierReservation> DossiersReservations { get; set; }

        [Display(Name="Email")]
        [NotMapped]
        public string EmailDisplay { get; set; }
    }
}