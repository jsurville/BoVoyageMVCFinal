﻿using BoVoyageMVC.Models;
using BoVoyageMVC.Services;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BoVoyageMVC.Controllers
{
    //[Route("MonCompte")]
    [Authorize(Roles = "Client")]
    public class ReservationsController : BaseController
    {
        // GET: Reservations
        public ActionResult Index()
        {
            var clientId = GetCurrentClientId();
            var dossiersReservations = db.DossiersReservations.Include(d => d.Client)               
                .Include(d => d.Voyage).Include(d => d.Voyage.Destination)
                 .Include(d => d.Participants)
                 .Include(d => d.Assurances)
                .Where(d => d.ClientId == clientId).ToList();
            return View(dossiersReservations);
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierReservation dossierReservation = db.DossiersReservations.Include(d => d.Client)
                .Include(d => d.Voyage).Include(d => d.Voyage.Destination)
                .Include(d => d.Participants)
                .Include(d => d.Assurances).SingleOrDefault(d => d.Id == id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }
            return View(dossierReservation);
        }

        // GET: Reservations/Book

        public ActionResult Book(int id)
        {
            var clientId = GetCurrentClientId();
            var voyage = db.Voyages.Include(v => v.Destination).SingleOrDefault(x => x.Id == id);
            if (voyage == null)
                return HttpNotFound();

            var dossierReservation = new DossierReservation();
            dossierReservation.VoyageId = id;
            dossierReservation.Voyage = voyage;
            dossierReservation.ClientId = clientId;
            dossierReservation.Client = db.Clients.Find(clientId);

            MultiSelectList assuranceValues = new MultiSelectList(db.Assurances, "ID", "TypeAssurance", db.Assurances.Select(x => x.ID));
            ViewBag.Assurances = assuranceValues;
            return View(dossierReservation);
        }


        // POST: Reservations/Book
        [Authorize(Roles = "Client")]
        [HttpPost]
        public ActionResult Book([Bind(Exclude = "Id")] DossierReservation dossierReservation, int[] AssuranceIds)
        {
            if (ModelState.IsValid)
            {
                if (AssuranceIds != null && AssuranceIds.Count() > 0)
                    dossierReservation.Assurances = db.Assurances.Where(x => AssuranceIds.Contains(x.ID)).ToList();
                int cardNumber = 0;
                if (int.TryParse(dossierReservation.CreditCardNumber, out cardNumber) && cardNumber > 0)
                {
                    dossierReservation.UnitPrice = db.Voyages.Find(dossierReservation.VoyageId).UnitPublicPrice;
                    db.DossiersReservations.Add(dossierReservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else
                {                   
                    ModelState.AddModelError("CreditCardNumber", "Carte Bancaire Invalide");                   
                }

            }
            var voyage = db.Voyages.Include(v => v.Destination).SingleOrDefault(x => x.Id == dossierReservation.VoyageId);
            dossierReservation.Voyage = voyage;
            MultiSelectList assuranceValues = new MultiSelectList(db.Assurances, "ID", "TypeAssurance", db.Assurances.Select(x => x.ID));
            ViewBag.Assurances = assuranceValues;

            return View(dossierReservation);
        }


      

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierReservation dossierReservation = db.DossiersReservations.Include(d => d.Client)
                .Include(d => d.Voyage).Include(d => d.Voyage.Destination)
                .Include(d => d.Participants)
                .Include(d => d.Assurances).SingleOrDefault(d => d.Id == id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }
            var clientId = GetCurrentClientId();
            if(dossierReservation.ClientId!=clientId)
            {
                Display("Vous n'avez pas accès à cette réservation", MessageType.ERROR);
                return RedirectToAction("Index");
            }
            if (dossierReservation.EtatDossier == EtatDossierReservation.Refusee
                || dossierReservation.EtatDossier == EtatDossierReservation.Annule)
            {
                Display("Votre réservation avait déjà été réfusé ou annulé",MessageType.ERROR);
                return RedirectToAction("Index");
            }
            return View(dossierReservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Include(d => d.Client)
                .Include(d => d.Voyage).Include(d => d.Voyage.Destination)
                .Include(d => d.Participants)
                .Include(d => d.Assurances).SingleOrDefault(d => d.Id == id);
            if (ModelState.IsValid)
            {
                if (dossierReservation.EtatDossier == EtatDossierReservation.Accepte)
                {
                    if (dossierReservation.Assurances.Where(x => x.TypeAssurance == TypeAssurance.Annulation).Count() > 0)
                    {
                        var rembourser = new CarteBancaireService().Rembourser(dossierReservation.CreditCardNumber,
                            dossierReservation.TotalPrice);
                        Display("Vous serez remboursé grâce à votre Assurance Annulation", MessageType.SUCCES);
                    }
                }
                dossierReservation.EtatDossier = EtatDossierReservation.Annule;
                dossierReservation.RaisonAnnulationDossier = RaisonAnnulationDossier.Client;


                db.Entry(dossierReservation).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
