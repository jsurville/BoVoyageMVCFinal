using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyageMVC.Services
{
    class CarteBancaireService
    {
        public bool ValiderSolvabilite(string numeroCarteBancaire, string prixTotal)
        {
            double solde = 0;
            double.TryParse(numeroCarteBancaire, out solde);
            double prix = 0;
            double.TryParse(prixTotal, out prix);
            return (solde > prix);  
        }

        public bool Rembourser(string numeroCarteBancaire, string prixTotal)
        {
            return true ;
        }

        public bool PayerAgence(object p)
        {
            return true ;
        }
    }
}
