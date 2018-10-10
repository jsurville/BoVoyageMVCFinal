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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FrontVoyages
        [Route("Voyages")]
        public ActionResult Index()
        {
            var voyages = db.Voyages.Include(v => v.AgenceVoyage).Include(v => v.Destination);
            return View(voyages.ToList());
        }

        // GET: FrontVoyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
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
