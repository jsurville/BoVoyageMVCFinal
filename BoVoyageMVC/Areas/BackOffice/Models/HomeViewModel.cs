using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoVoyageMVC.Models;

namespace BoVoyageMVC.Areas.BackOffice.Models
{
  
        public class HomeIndexViewModel
        {
            public IEnumerable<Voyage> Voyages { get; set; }
        }
  
}
