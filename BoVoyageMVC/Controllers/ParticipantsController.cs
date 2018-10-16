using BoVoyageMVC.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace BoVoyageMVC.Controllers
{
    [Authorize(Roles = "Client")]
    public class ParticipantsController : BaseController
    {
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // GET: Participants/Create
        public ActionResult Create(int? id)
        {
            if (id == null || id == 0)
            {
                Display("Réservation inexistante", MessageType.ERROR);
                return RedirectToAction("Index", "Reservations");
            }
            var dossierReservation = db.DossiersReservations.Find(id);
            var clientId = GetCurrentClientId();
            if (dossierReservation.ClientId != clientId)
            {
                Display("Vous n'avez pas accès à cette réservation", MessageType.ERROR);
                return RedirectToAction("Index", "Reservations");
            }
            if (dossierReservation.EtatDossier != EtatDossierReservation.EnAttente)
            {
                Display("Votre réservation doit etre en attente pour pouvoir ajouter des participants", MessageType.ERROR);
                return RedirectToAction("Index", "Reservations");
            }

            var participant = new Participant();
            participant.DossierReservationId = (int)id;

            return View(participant);
        }

        // POST: Participants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DossierReservationId,Title,LastName,FisrtName,Address,PhoneNumber,BirthDate")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Participants.Add(participant);
                db.SaveChanges();
                return RedirectToAction("Index", "Reservations");
            }
            return View(participant);
        }

        //// GET: Participants/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Participant participant = db.Participants.Find(id);
        //    if (participant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DossierReservationId = new SelectList(db.DossiersReservations, "Id", "CreditCardNumber", participant.DossierReservationId);
        //    return View(participant);
        //}

        // POST: Participants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,DossierReservationId,Title,LastName,FisrtName,Address,PhoneNumber,BirthDate")] Participant participant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(participant).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.DossierReservationId = new SelectList(db.DossiersReservations, "Id", "CreditCardNumber", participant.DossierReservationId);
        //    return View(participant);
        //}

        // GET: Participants/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Participant participant = db.Participants.Find(id);
        //    if (participant == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(participant);
        //}

        //// POST: Participants/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Participant participant = db.Participants.Find(id);
        //    db.Participants.Remove(participant);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
