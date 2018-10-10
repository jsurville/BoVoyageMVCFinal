using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Controllers;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [RouteArea("BackOffice")]
    [Authorize(Roles = "Commercial")]
    public class ClientsController : BaseController
    {
        // GET: BackOffice/Clients
        public ActionResult Index()
        {
            return View();
        }

        // GET: BackOffice/Clients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BackOffice/Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BackOffice/Clients/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BackOffice/Clients/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BackOffice/Clients/Edit/5
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

        // GET: BackOffice/Clients/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BackOffice/Clients/Delete/5
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
