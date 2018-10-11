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
    
    public class FrontVoyagesController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        // GET: FrontVoyages
        [Route("Voyages")]
        public ActionResult Index()
        {
            var voyages = db.Voyages.Include("Destination").ToList();
            return View(voyages);
        }

        // GET: FrontVoyages/Details/5
        [Route("voyage-details")]  
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Include("Destination").SingleOrDefault(x => x.Id == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
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
