using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Controllers;
using BoVoyageMVC.Models;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Commercial")]
    public class DestinationsController : BaseController
    {
        // GET: BackOffice/Destinations
        public ActionResult Index()
        {
            return View(db.Destinations.ToList());
        }

        // GET: BackOffice/Destinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // GET: BackOffice/Destinations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Destinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Continent,Country,Region,Description")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        // GET: BackOffice/Destinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Include("Images").SingleOrDefault(x => x.Id == id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: BackOffice/Destinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Continent,Country,Region,Description,Images")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                destination = db.Destinations.Include("Images").SingleOrDefault(x => x.Id == destination.Id);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }


        [HttpPost]
        public ActionResult AddPicture(HttpPostedFileBase picture, int id)
        {
            if (picture?.ContentLength > 0)
            {
                var tp = new Image();
                tp.ContentType = picture.ContentType;
                tp.Name = picture.FileName;
                tp.DestinationId = id;

                using (var reader = new BinaryReader(picture.InputStream))
                {
                    tp.Content = reader.ReadBytes(picture.ContentLength);
                }
                db.Images.Add(tp);
                db.SaveChanges();
                return RedirectToAction("edit", "destinations", new { id = id });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        // GET
        public ActionResult DeletePicture(int id, int iddestination)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.Entry(image).State = EntityState.Deleted;
            db.SaveChanges();
            // return Json(image);
            return RedirectToAction("Edit", "Destinations", new { id = iddestination });
        }


        // GET: BackOffice/Destinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            var voyages = db.Voyages.Where(x => x.DestinationId == destination.Id);
            if (voyages != null)
            {
                Display("Impossible de supprimer une Destination pour un Voyage en Cours ", type: MessageType.ERROR);
            }
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: BackOffice/Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Destination destination = db.Destinations.Include("Images").SingleOrDefault(x => x.Id == id);
            var voyages = db.Voyages.Where(x => x.DestinationId == id);
            if(voyages != null)
            {
                Display("Impossible de supprimer une Destination pour un Voyage en Cours ", type: MessageType.ERROR );
            }
            else
            {
                foreach (var item in voyages)
                {
                    db.Entry(item).State = EntityState.Deleted;  // équivalent à db.Shooters.Remove(item);
                }

                db.Destinations.Remove(destination);

                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

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
