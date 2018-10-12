using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Models;
using BoVoyageMVC.Controllers;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Commercial,Admin")]
    public class AssurancesController : BaseController
    {
        

        // GET: BackOffice/Assurances
        public ActionResult Index()
        {
            return View(db.Assurances.ToList());
        }

        // GET: BackOffice/Assurances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = db.Assurances.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
            return View(assurance);
        }

        // GET: BackOffice/Assurances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Assurances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Montant,TypeAssurance")] Assurance assurance)
        {
            if (assurance.Montant < 0)
            { 
                Display("Assurance doit etre possitive");
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Assurances.Add(assurance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assurance);
        }

        // GET: BackOffice/Assurances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = db.Assurances.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
            return View(assurance);
        }

        // POST: BackOffice/Assurances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Montant,TypeAssurance")] Assurance assurance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assurance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assurance);
        }

        // GET: BackOffice/Assurances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assurance assurance = db.Assurances.Find(id);
            if (assurance == null)
            {
                return HttpNotFound();
            }
           // if(db.DossiersReservations.Any(d=>d.Assurances. ))
            return View(assurance);
        }

        // POST: BackOffice/Assurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assurance assurance = db.Assurances.Find(id);
            db.Assurances.Remove(assurance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
