using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BoVoyageMVC.Models
{
	public enum EtatDossierReservation : byte
	{
		[EnumMember(Value = "En Attente")]
		EnAttente,

		[EnumMember(Value = "En Cours")]
		EnCours,

		[EnumMember(Value = "Refusee")]
		Refusee,

		[EnumMember(Value = "Acceptee")]
		Acceptee
	}

	public enum RaisonAnnulationDossier : byte
	{
		[EnumMember(Value = "Client")]
		Client = 1,

		[EnumMember(Value = "Places Insuffisantes")]
		PlacesInsuffisantes
	}

	[Table("DossiersReservations")]
	public class DossierReservation
	{
		public int Id { get; set; }

        [Display(Name = "Numéro Dossier")]
        [NotMapped]
		public int UniqueNumber => Id+1000;

        [Display(Name = "Numéro CB")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[StringLength(20, MinimumLength = 5, ErrorMessage = "La numero de CB doit avoir de 5 a 20 caracteres")]
		public string CreditCardNumber { get; set; }

        [Display(Name = "Prix/client")]
        public double UnitPrice
        {
            get
            {
                return Voyage.UnitPublicPrice;
            }
            set { value= Voyage.UnitPublicPrice; }
        }
		
        [Display(Name ="Prix Total")]
		[NotMapped]
        public double TotalPrice {
            get
            {
                double totalPrice = 0;
                foreach (var participant in this.Participants)
                {
                    totalPrice += (1 - (double)participant.Discount) * UnitPrice;
                }

                foreach (var assurance in this.Assurances)
                {
                    if (assurance.TypeAssurance == TypeAssurance.Annulation)
                    {
                        totalPrice += (double)assurance.Montant;
                    }
                }
                return totalPrice;
            }
        }

        [Display(Name ="Etat Dossier")]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[EnumDataType(typeof(EtatDossierReservation))]
		[JsonConverter(typeof(StringEnumConverter))]
		public EtatDossierReservation EtatDossier { get; set; }

        [Display(Name ="Client")]
		public int ClientId { get; set; }

		[ForeignKey("ClientId")]
		public Client Client { get; set; }

        [Display(Name = "Voyage")]
        public int VoyageId { get; set; }

		[ForeignKey("VoyageId")]
		public Voyage Voyage { get; set; }

		public ICollection<Assurance> Assurances { get; set; }

		public ICollection<Participant> Participants { get; set; }
	}
}