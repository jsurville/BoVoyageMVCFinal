using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Controllers;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Commercial")]
    public class CommercialsController : BaseController
    {
        // GET: BackOffice/Commercials
        public ActionResult Index()
        {
            var commercials = db.Commercials;
            return View(commercials.ToList());
            
        }

        

        // GET: BackOffice/Commercials/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BackOffice/Commercials/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BackOffice/Commercials/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BackOffice/Commercials/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
