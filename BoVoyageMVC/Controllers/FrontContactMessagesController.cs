using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Models;

namespace BoVoyageMVC.Controllers
{
    public class FrontContactMessagesController : BaseController
    {
        // GET: ContactMessages/Create
        //[Route("Nous-contacter")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,LastName,FisrtName,Email,PhoneNumber,Message")] ContactMessage contactMessage)
        {
            contactMessage.SendDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.ContactMessages.Add(contactMessage);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(contactMessage);
        }
    }
}
