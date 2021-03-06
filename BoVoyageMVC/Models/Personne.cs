﻿using BoVoyageMVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Models
{
	public abstract class Personne
	{
		public int Id { get; set; }

        [Display(Name = "Civilité")]
        [RegularExpression(@"([a-zA-Z])*\s*$", ErrorMessage = "Le champ {0} ne doit contenir que des lettres")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[StringLength(20, MinimumLength = 1, ErrorMessage = "La civilite doit avoir de 1 à 20 caractères")]
		[Index("IX_PersonneUnique", 1, IsUnique = true)]
		public string Title { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Le nom doit avoir de 2 à 30 caractères")]
		[Index("IX_PersonneUnique", 2, IsUnique = true)]
		public string LastName { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "La prénom doit avoir de 2 à 30 caractères")]
		[Index("IX_PersonneUnique", 3, IsUnique = true)]
		public string FisrtName { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
		[StringLength(60, MinimumLength = 5, ErrorMessage = "L'adresse doit avoir de 5 à 60 caractères")]
		[Index("IX_PersonneUnique", 4, IsUnique = true)]
		public string Address { get; set; }

        [Display(Name = "Téléphone")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [RegularExpression(@"^([0-9])*\s*$",ErrorMessage = "Le champ {0} ne doit contenir que des chiffres")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Le numéro de telephone doit avoir de 3 à 20 caractères")]
		public string PhoneNumber { get; set; }

        [Display(Name = "Date de Naissance")]
        [DataType(DataType.DateTime)]
		[Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Column(TypeName ="datetime2")]
        [Age(0, MaximumAge = 110, ErrorMessage = "Pour le champ {0}, vous devez avoir plus de {1} ans")]
        public DateTime BirthDate { get; set; }

		[NotMapped]
		public int Age
		{
			get
			{
				DateTime today = DateTime.Today;
				int age = today.Year - BirthDate.Year;
				return age;
			}
		}
	}
}