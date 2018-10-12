using BoVoyageMVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Models
{
    public class Dashboard
    {
        
        public ICollection<Voyage> Voyages { get; set; }

        public ICollection<DossierReservation> DossiersReservations { get; set; }

    }
}