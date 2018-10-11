using BoVoyageMVC.Models;
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
            var dossiersReservations = db.DossiersReservations.Include(d => d.Client)
                .Include(d => d.Voyage).Include(d => d.Voyage.Destination);
            return View(dossiersReservations.ToList());
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
                .SingleOrDefault(d => d.Id == id);
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
                dossierReservation.UnitPrice = db.Voyages.Find(dossierReservation.VoyageId).UnitPublicPrice;
                db.DossiersReservations.Add(dossierReservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            MultiSelectList assuranceValues = new MultiSelectList(db.Assurances, "ID", "TypeAssurance", db.Assurances.Select(x => x.ID));
            ViewBag.Assurances = assuranceValues;

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
