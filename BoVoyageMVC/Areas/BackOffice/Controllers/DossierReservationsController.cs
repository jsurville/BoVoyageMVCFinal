using BoVoyageMVC.Controllers;
using BoVoyageMVC.Models;
using BoVoyageMVC.Services;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{

    [RouteArea("BackOffice")]
    [Authorize(Roles = "Commercial,Admin")]
    public class DossierReservationsController : BaseController
    {
        // GET: BackOffice/DossierReservations
        [Route("Bookings")]
        public ActionResult Index()
        {
            var dossiersReservations = db.DossiersReservations.Include(d => d.Client).Include(z => z.Participants).Include(d => d.Voyage).Include(u => u.Voyage.Destination);
            return View(dossiersReservations.ToList());
        }

        // GET: BackOffice/DossierReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DossierReservation dossierReservation = db.DossiersReservations.Include(y => y.Voyage).Include(z => z.Client)
                                                    .Include(t => t.Participants).Include(k => k.Assurances)
                                                    .Include(t => t.Voyage.Destination).SingleOrDefault(x => x.Id == id);
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
                Display("L'état du Dossier ne permet pas de valider ou a déjà été validé", MessageType.ERROR);
                return RedirectToAction("Index");
            }
            var carteBancaireServie = new CarteBancaireService();
            if (carteBancaireServie.ValiderSolvabilite(dossierReservation.CreditCardNumber,
                dossierReservation.TotalPrice))
            {
                dossierReservation.EtatDossier = EtatDossierReservation.EnCours;
                db.Entry(dossierReservation).State = EntityState.Modified;
                db.SaveChanges();
                Display("La réservation a été validée", MessageType.SUCCES);
            }
            else
            {
                dossierReservation.EtatDossier = EtatDossierReservation.Refusee;
                dossierReservation.RaisonAnnulationDossier = RaisonAnnulationDossier.Insolvable;
                Display("Opération Refusée pour insolvabilité", MessageType.ERROR);
            }
            return RedirectToAction("Index");
        }

        // POST: BackOffice/DossierReservations/
        public ActionResult Accept(int? id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }
            if (dossierReservation.EtatDossier != EtatDossierReservation.EnCours)
            {
                Display("L'état du Dossier ne permet pas de valider");
                return RedirectToAction("Index");
            }

            var voyage = db.Voyages.Find(dossierReservation.VoyageId);
            if (dossierReservation.Participants.Count < voyage.MaxCapacity)
            {                
                dossierReservation.EtatDossier = EtatDossierReservation.Accepte;               
                voyage.MaxCapacity -= dossierReservation.Participants.Count;
                db.Entry(voyage).State = EntityState.Modified;
                Display("La réservation a été acceptée");
            }
            else
            {
                dossierReservation.EtatDossier = EtatDossierReservation.Refusee;
                dossierReservation.RaisonAnnulationDossier = RaisonAnnulationDossier.PlacesInsuffisantes;
                Display("La réservation a été refusée pour cause places non disponibles");
            }

            db.Entry(dossierReservation).State = EntityState.Modified;           
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: BackOffice/DossierReservations/
        public ActionResult Cancel(int? id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation == null)
            {
                return HttpNotFound();
            }
            if (dossierReservation.EtatDossier == EtatDossierReservation.Clos
               || dossierReservation.EtatDossier == EtatDossierReservation.Annule)
            {
                Display("L'état du Dossier ne permet pas de l'Annuler", MessageType.ERROR);
                return RedirectToAction("Index");
            }
            if (dossierReservation.EtatDossier != EtatDossierReservation.Refusee)
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
                dossierReservation.RaisonAnnulationDossier = RaisonAnnulationDossier.Client;
                dossierReservation.EtatDossier = EtatDossierReservation.Annule;
                db.Entry(dossierReservation).State = EntityState.Modified;
                db.SaveChanges();
                Display("La réservation a été annulée", MessageType.SUCCES);
            }

            return RedirectToAction("Index");
        }

        // POST: BackOffice/DossierReservations/
        public ActionResult Close(int? id)
        {
            DossierReservation dossierReservation = db.DossiersReservations.Find(id);
            if (dossierReservation.EtatDossier != EtatDossierReservation.Accepte)
            {
                Display("L'état du Dossier ne permet pas de le Clotûrer", MessageType.ERROR);
                return RedirectToAction("Index");
            }
            dossierReservation.EtatDossier = EtatDossierReservation.Clos;
            db.Entry(dossierReservation).State = EntityState.Modified;
            db.SaveChanges();
            Display("Le Dossier de Réservation a été clôturé", MessageType.SUCCES);
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

    }
}
