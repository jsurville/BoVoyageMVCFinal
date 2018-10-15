using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Models;

namespace BoVoyageMVC.Controllers
{    
    public class FrontVoyagesController : BaseController
    {
        // GET: FrontVoyages
        [Route("Voyages")]
        public ActionResult Index()
        {
            var voyages = db.Voyages.Include("Destination").Include(x => x.Destination.Images).ToList();
            return View(voyages);
        }

        // GET: FrontVoyages/Details/5
        [Route("voyage-{region}-{country}/{id}")]  
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Include("Destination").Include(y => y.Destination.Images).SingleOrDefault(x => x.Id == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // GET: FrontVoyages/Search/
         [Route("Search")]
        public ActionResult Search(string search, DateTime? departureDate, int? maxprice)
        {

            IEnumerable<Voyage> voyages = db.Voyages.Include(u => u.AgenceVoyage).
                Include(x => x.Destination).Include(x => x.Destination.Images);

            if (!string.IsNullOrWhiteSpace(search))
            {
                voyages = voyages.Where(x => x.Destination.Description.Contains(search)
            || x.Destination.Continent.Contains(search)
            || x.Destination.Country.Contains(search)
            || x.Destination.Region.Contains(search));
            }

            if (departureDate != null)
                voyages = voyages.Where(x => x.DepartureDate <= departureDate);


            if (maxprice != null)
                voyages = voyages.Where(x => x.UnitPublicPrice <= maxprice);

            if (voyages.Count() == 0)
            {
                Display("Aucun résultat");
            }

            return View("Index", voyages.ToList());

        }
    }
}
