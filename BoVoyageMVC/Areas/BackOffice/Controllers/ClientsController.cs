﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Controllers;
using BoVoyageMVC.Models;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [RouteArea("BackOffice")]
    [Authorize(Roles = "Commercial")]
    public class ClientsController : BaseController
    {
        // GET: BackOffice/Clients
        public ActionResult Index()
        {
            
            List<Client> clients = db.Clients.ToList();
            return View(clients);
        }

        // GET: BackOffice/Clients/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: BackOffice/Clients/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: BackOffice/Clients/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Title,LastName,FirstName,Address,PhoneNumber,BirthDate")] Client client)
        {
            if (client.BirthDate <= DateTime.Now)

            {
                Display("Date de naissance est invalide");
            }
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
   
                return View();
            
        }

        // GET: BackOffice/Clients/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
         
            return View(client);
            
        }
        public ActionResult Search(string search)
        {
            if (search == null)
            {
                return RedirectToRoute("Index");
            }
            ICollection<Client> clients = db.Clients.Where(x => x.LastName.Contains(search) || x.Address.Contains(search)).ToList();
            //var voyages = db.Voyages.Include("Destination").Include(x => x.Destination.Images).ToList();
            if (clients?.Count() == 0)
            {
                Display("Aucun Résultat ");
            }
            else
            {
                return View(clients);
            }
            return RedirectToRoute("Index");

            //return RedirectToAction("Index", "Home");

        }



    }
}
