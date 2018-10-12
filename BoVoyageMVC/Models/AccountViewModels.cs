using BoVoyageMVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoVoyageMVC.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "M/Mme")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "La civilite doit avoir de 1 à 20 caractères")]
        public string Title { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Le nom doit avoir de 2 à 30 caractères")]
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "La prénom doit avoir de 2 à 30 caractères")]
        public string FisrtName { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "L'adresse doit avoir de 5 à 60 caractères")]
        public string Address { get; set; }

        [Display(Name = "Téléphone")]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [RegularExpression(@"^([0-9])*\s*$")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Le numéro de telephone doit avoir de 3 à 20 caractères")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date de Naissance")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Le champ {0} est obligatoire.")]
        [Age(18, MaximumAge = 110, ErrorMessage = "Pour le champ {0}, vous devez avoir plus de {1} ans")]
        public DateTime BirthDate { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
