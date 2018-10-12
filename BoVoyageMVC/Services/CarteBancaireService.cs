using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyageMVC.Services
{
    class CarteBancaireService
    {
        public bool ValiderSolvabilite(string numeroCarteBancaire, double prixTotal)
        {
            double solde = 0;
            double.TryParse(numeroCarteBancaire, out solde);
            return (solde > prixTotal);  
        }

        public bool Rembourser(string numeroCarteBancaire, decimal prixTotal)
        {
            return true ;
        }

        public bool PayerAgence(object p)
        {
            return true ;
        }
    }
}
