using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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

        [EnumMember(Value = "Accepte")]
        Accepte,

        [EnumMember(Value = "Annule")]
        Annule
    }

    public enum RaisonAnnulationDossier : byte
    {
        [EnumMember(Value = "Client")]
        Client =1,

        [EnumMember(Value = "Places Insuffisantes")]
        PlacesInsuffisantes,

        [EnumMember(Value = "Insolvable")]
        Insolvable
    }

    [Table("DossiersReservations")]
    public class DossierReservation
    {
        public int Id { get; set; }

        [Display(Name = "Numéro Dossier")]
        [NotMapped]
        public int UniqueNumber => Id + 1000;

        [Display(Name = "Numéro CB")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La numero de CB doit avoir de 5 a 20 caracteres")]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Prix/client")]
        public double UnitPrice { get; set; }

        public DossierReservation()
        {
            this.Participants = new HashSet<Participant>();
            this.Assurances = new HashSet<Assurance>();
        }

        [Display(Name = "Prix Total")]
        [NotMapped]
        public int TotalPrice
        {
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
                return (int)totalPrice;
            }
        }

        [Display(Name = "Etat Dossier")]
        [EnumDataType(typeof(EtatDossierReservation))]
        [JsonConverter(typeof(StringEnumConverter))]
        public EtatDossierReservation EtatDossier { get; set; }

        [Display(Name = "Raison Annulation")]
        [EnumDataType(typeof(RaisonAnnulationDossier))]
        [JsonConverter(typeof(StringEnumConverter))]
        public RaisonAnnulationDossier? RaisonAnnulationDossier { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [Display(Name = "Voyage")]
        public int VoyageId { get; set; }

        [ForeignKey("VoyageId")]
        public Voyage Voyage { get; set; }

        [Display(Name = "Assurances")]
        public ICollection<Assurance> Assurances { get; set; }

        [Display(Name = "Participants")]
        public ICollection<Participant> Participants { get; set; }
    }
}