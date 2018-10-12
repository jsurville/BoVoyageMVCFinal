using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Models;
using BoVoyageMVC.Tools;
using BoVoyageMVC.Controllers;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [RouteArea("BackOffice")]
    [Authorize(Roles = "Commercial,Admin")]
    public class VoyagesController : BaseController
    {
       
        // GET: BackOffice/Voyages
        public ActionResult Index()
        {
            var voyages = db.Voyages.Include(v => v.AgenceVoyage).Include("Destination");
            return View(voyages.ToList());
        }
        
        // GET: BackOffice/Voyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Include("Destination").Include(x => x.Destination.Images).Include(u => u.AgenceVoyage).SingleOrDefault(x=>x.Id == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);

        }

        
        // GET: BackOffice/Voyages/Create
        public ActionResult Create()
        {
            ViewBag.AgenceVoyageId = new SelectList(db.AgencesVoyages, "Id", "Name");
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Name");
           
            return View();
        }

        // POST: BackOffice/Voyages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartureDate,ReturnDate,MaxCapacity,UnitPrice,Margin,AgenceVoyageId,DestinationId")] Voyage voyage)
        {
            if (voyage.ReturnDate <= voyage.DepartureDate)

            {
                Display("Date retour est invalide"); 
            }
            if (ModelState.IsValid)
            {
                db.Voyages.Add(voyage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgenceVoyageId = new SelectList(db.AgencesVoyages, "Id", "Name", voyage.AgenceVoyageId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Continent", voyage.DestinationId);
            return View(voyage);
        }

        // GET: BackOffice/Voyages/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AgenceVoyageId = new SelectList(db.AgencesVoyages, "Id", "Name", voyage.AgenceVoyageId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Continent", voyage.DestinationId);
            return View(voyage);
        }

        // POST: BackOffice/Voyages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartureDate,ReturnDate,MaxCapacity,UnitPrice,Margin,AgenceVoyageId,DestinationId")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voyage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgenceVoyageId = new SelectList(db.AgencesVoyages, "Id", "Name", voyage.AgenceVoyageId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Continent", voyage.DestinationId);
            return View(voyage);
        }


        // GET: BackOffice/Search/
        [Route("Search")]
        public ActionResult Search(string search, DateTime? departureDate, int? maxprice)
        {
            if (search != "" && departureDate == null && maxprice == null)
            {
                ICollection<Voyage> voyages = db.Voyages.Include(x => x.Destination).Include(x => x.Destination.Images)
            .Where(x => x.Destination.Description.Contains(search)
            || x.Destination.Continent.Contains(search)
            || x.Destination.Country.Contains(search)
            || x.Destination.Region.Contains(search)).ToList();

                if (voyages != null)
                {
                    return View(voyages);
                }
            }
            if (search == "" && departureDate != null && maxprice == null)
            {

                ICollection<Voyage> voyages = db.Voyages.Include(x => x.Destination).Include(x => x.Destination.Images)
            .Where(x => x.DepartureDate <= departureDate).ToList();

                if (voyages != null)
                {
                    return View(voyages);
                }
            }
            if (search == "" && departureDate == null && maxprice != null)
            {

                ICollection<Voyage> voyages = db.Voyages.Include(x => x.Destination).Include(x => x.Destination.Images)
            .ToList().Where(x => x.UnitPrice <= maxprice).ToList();

                if (voyages != null)
                {
                    return View(voyages);
                }
            }


            Display("Aucun résultat");
            Display("Le Nouveau Tireur a bien été enregistré");
            return RedirectToAction("Index", "Voyages");


        }

        // GET: BackOffice/Voyages/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: BackOffice/Voyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            db.Voyages.Remove(voyage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
