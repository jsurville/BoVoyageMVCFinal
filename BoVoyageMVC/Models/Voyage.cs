using BoVoyageMVC.Validators;
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
       // [DecimalAtribute(ErrorMessage = "Le champ {0} doit etre positif.")]
        [Display(Name = "Prix/pers")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        public double UnitPrice { get; set; }

        [Range(1, 100)]
        //[Range(1, 3)]
        //[Range(0, 9999999999999999.99, ErrorMessage = "Invalid Target Price; Max 18 digits")]

        //[RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Target Price; Maximum Two Decimal Points.")]
        //[Range(1, (double)decimal.MaxValue, ErrorMessage = "value should be between{1} and {2}."]
        //[DecimalAtribute(ErrorMessage = "Le champ {0} doit etre positif.")]
        [Display(Name = "Marge")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        public int Margin { get; set; }

        [Display(Name ="Prix/client")]
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:C0}")]
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