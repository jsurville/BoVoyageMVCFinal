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
    [Authorize(Roles = "Commercial")]
    public class AgenceVoyagesController : BaseController
    {
        // GET: BackOffice/AgenceVoyages
        public ActionResult Index()
        {
            return View(db.AgencesVoyages.ToList());
        }

        // GET: BackOffice/AgenceVoyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return HttpNotFound();
            }
            return View(agenceVoyage);
        }

        // GET: BackOffice/AgenceVoyages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/AgenceVoyages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AgenceVoyage agenceVoyage)
        {
            if (ModelState.IsValid)
            {
                db.AgencesVoyages.Add(agenceVoyage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agenceVoyage);
        }

        // GET: BackOffice/AgenceVoyages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return HttpNotFound();
            }
            return View(agenceVoyage);
        }

        // POST: BackOffice/AgenceVoyages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AgenceVoyage agenceVoyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agenceVoyage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agenceVoyage);
        }

        // GET: BackOffice/AgenceVoyages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
            if (agenceVoyage == null)
            {
                return HttpNotFound();
            }
            return View(agenceVoyage);
        }

        // POST: BackOffice/AgenceVoyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgenceVoyage agenceVoyage = db.AgencesVoyages.Find(id);
            db.AgencesVoyages.Remove(agenceVoyage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
