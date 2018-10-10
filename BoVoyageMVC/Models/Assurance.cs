using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace BoVoyageMVC.Models
{
	public enum TypeAssurance : byte
	{
        [EnumMember(Value = "Annulation")]
        Annulation = 1
	}

	public class Assurance
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[Column(TypeName = "Money")]
		[Index("IX_MontantType", 1, IsUnique = true)]
		public decimal Montant { get; set; }

		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[EnumDataType(typeof(TypeAssurance))]
		[Index("IX_MontantType", 2, IsUnique = true)]
		[JsonConverter(typeof(StringEnumConverter))]
		public TypeAssurance TypeAssurance { get; set; }

		public ICollection<DossierReservation> DossiersReservations { get; set; }
	}
}