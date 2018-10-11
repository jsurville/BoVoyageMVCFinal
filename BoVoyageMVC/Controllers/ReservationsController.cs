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
    //[Route("MonCompte")]
    public class ReservationsController : BaseController
    {       

        // GET: Reservations
        public ActionResult Index()
        {
            var dossiersReservations = db.DossiersReservations.Include(d => d.Client).Include(d => d.Voyage);
            return View(dossiersReservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }
            return View(dossierReservation);
        }



        // GET: Reservations/Book
        [Authorize(Roles = "Client")]
        public ActionResult Book(int id, int? clientId)
        {
            var voyage = db.Voyages.Find(id);
            if (voyage == null)
                return HttpNotFound();
            if(clientId == null || clientId == 0)
                return RedirectToAction("Index","Home");
            var dossierReservation = new DossierReservation();
            dossierReservation.VoyageId = id;
            dossierReservation.Voyage = voyage;
            dossierReservation.ClientId = (int)clientId;
            return View(dossierReservation);
        }


        // POST: Reservations/Book
        [Authorize(Roles ="Client")]
        [HttpPost]
        public ActionResult Book(int? id)
        {
            
            return View();
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "UserId");
            ViewBag.VoyageId = new SelectList(db.Voyages, "Id", "Id");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreditCardNumber,UnitPrice,EtatDossier,ClientId,VoyageId")] DossierReservation dossierReservation)
        {
            if (ModelState.IsValid)
            {
                db.DossiersReservations.Add(dossierReservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "UserId", dossierReservation.ClientId);
            ViewBag.VoyageId = new SelectList(db.Voyages, "Id", "Id", dossierReservation.VoyageId);
            return View(dossierReservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "UserId", dossierReservation.ClientId);
            ViewBag.VoyageId = new SelectList(db.Voyages, "Id", "Id", dossierReservation.VoyageId);
            return View(dossierReservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreditCardNumber,UnitPrice,EtatDossier,ClientId,VoyageId")] DossierReservation dossierReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dossierReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "UserId", dossierReservation.ClientId);
            ViewBag.VoyageId = new SelectList(db.Voyages, "Id", "Id", dossierReservation.VoyageId);
            return View(dossierReservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }
            return View(dossierReservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            db.DossiersReservations.Remove(dossierReservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
