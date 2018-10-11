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
        protected ApplicationDbContext db = new ApplicationDbContext();

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
        public ActionResult Search(string search, DateTime? departureDate, DateTime? returnDate)
        {
            if (search == null)
            {
                return RedirectToRoute("Index");
            }
            ICollection<Voyage> voyages = db.Voyages.Include(x => x.Destination).Include(x => x.Destination.Images)
            .Where(x => x.Destination.Description.Contains(search)
            || x.Destination.Continent.Contains(search)
            || x.Destination.Country.Contains(search)
            || x.Destination.Region.Contains(search)
             || (x.DepartureDate > departureDate
            && x.ReturnDate < returnDate)).ToList();
               
                if (voyages?.Count() == 0)
                {
                    Display("Aucun Résultat ");
                }
                else
                {
                    return View(voyages);
                }
                return RedirectToRoute("Index", "Home");

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
