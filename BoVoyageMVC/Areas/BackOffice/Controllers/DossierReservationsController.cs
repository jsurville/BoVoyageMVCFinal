using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Controllers;
using BoVoyageMVC.Models;
using BoVoyageMVC.Services;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [Authorize(Roles="Commercial")]
    [RouteArea("BackOffice")]
    public class DossierReservationsController : BaseController
    {
        

        // GET: BackOffice/DossierReservations
        [Route("Bookings")]
        public ActionResult Index()
        {
            var dossiersReservations = db.DossiersReservations.Include(d => d.Client).Include(d => d.Voyage).Include(u => u.Voyage.Destination);
            return View(dossiersReservations.ToList());
        }

        // GET: BackOffice/DossierReservations/Details/5
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

        // GET: BackOffice/DossierReservations/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "UserId");
            ViewBag.VoyageId = new SelectList(db.Voyages, "Id", "Id");
            return View();
        }

        // POST: BackOffice/DossierReservations/Create
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

        // GET: BackOffice/DossierReservations/Edit/5
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

        // POST: BackOffice/DossierReservations/Edit/5
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

        // POST: BackOffice/DossierReservations/
        
        public ActionResult Validate(int? id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }

            if (dossierReservation.EtatDossier != EtatDossierReservation.EnAttente)
            {
                Display("L'état du Dossier ne permet pas de valider");
                return RedirectToAction("Index");
            }
            var carteBancaireServie = new CarteBancaireService();
            if (carteBancaireServie.ValiderSolvabilite(dossierReservation.CreditCardNumber,
                dossierReservation.TotalPrice))
            {
                dossierReservation.EtatDossier = EtatDossierReservation.EnCours;
                Display("La réservation a été validée");
            }
            else
            {
                dossierReservation.EtatDossier = EtatDossierReservation.Refusee;
                dossierReservation.RaisonAnnulationDossier = RaisonAnnulationDossier.Insolvable;
                Display("Opération Refusée pour insolvabilité");
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }










        //// GET: BackOffice/DossierReservations/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DossierReservation dossierReservation = db.DossiersReservations.Find(id);
        //    if (dossierReservation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dossierReservation);
        //}

        //// POST: BackOffice/DossierReservations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DossierReservation dossierReservation = db.DossiersReservations.Find(id);
        //    db.DossiersReservations.Remove(dossierReservation);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
