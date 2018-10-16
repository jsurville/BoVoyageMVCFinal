using BoVoyageMVC.Controllers;
using BoVoyageMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [RouteArea("BackOffice")]
    [Authorize(Roles = "Commercial,Admin")]
    public class VoyagesController : BaseController
    {

        // GET: BackOffice/Voyages
        public ActionResult Index()
        {
            List<Voyage> voyages = db.Voyages.Include(v => v.AgenceVoyage).Include("Destination").ToList();
            return View(voyages);
        }

        // GET: BackOffice/Voyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Include("Destination").Include(x => x.Destination.Images).Include(u => u.AgenceVoyage).SingleOrDefault(x => x.Id == id);
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

            if (ModelState.IsValid)
            {
                if (voyage.ReturnDate >= voyage.DepartureDate.AddDays(2))
                {
                    db.Voyages.Add(voyage);
                    db.SaveChanges();
                    Display("Le Nouveau Voyage a bien été créé");
                    return RedirectToAction("Index");
                }
                else
                {
                    Display("La Date de retour n'est pas valide", MessageType.ERROR);
                }
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
            Voyage voyage = db.Voyages.Include(t => t.DossiersReservations).SingleOrDefault(y => y.Id == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            if (voyage.DossiersReservations?.Count() > 0)
            {
                Display("Impossible, Dossier en cours", MessageType.ERROR);
                return RedirectToAction("Index");
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
                voyages = voyages.Where(x => x.UnitPrice <= maxprice);            

            if (voyages.Count() == 0)
            {
                Display("Aucun résultat");
            }

            return View("Index", voyages.ToList());
        }

        // GET: BackOffice/Voyages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Include(t => t.DossiersReservations).Include(x => x.AgenceVoyage).Include(x => x.Destination).SingleOrDefault(y => y.Id == id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            if (voyage.DossiersReservations?.Count() > 0)
            {
                Display("Impossible, Dossier en cours", MessageType.ERROR);
                return RedirectToAction("Index");
            }

            return View(voyage);
        }

        // POST: BackOffice/Voyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voyage voyage = db.Voyages.Include(t => t.DossiersReservations).Include(x => x.AgenceVoyage).Include(x => x.Destination).SingleOrDefault(y => y.Id == id);

            db.Voyages.Remove(voyage);
            db.SaveChanges();

            Display("Le Voyage a été supprimé", MessageType.SUCCES);

            return RedirectToAction("Index");
        }
    }
}
