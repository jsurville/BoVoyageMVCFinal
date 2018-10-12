using System;
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
        public ActionResult Details(int? id)
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

        
        public ActionResult Edit(int id)
        {
            if (id == 0)
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
            if (search != "" )
            {
                ICollection<Client> clients = db.Clients.Where(x => x.LastName.Contains(search) || x.FisrtName.Contains(search) || x.Address.Contains(search) || x.Address.Contains(search)).ToList();
                //var voyages = db.Voyages.Include("Destination").Include(x => x.Destination.Images).ToList();
                if (clients?.Count() == 0)
                {
                    Display("Aucun Résultat ");
                }
                return View(clients);
            }

            return RedirectToAction("Index", "Clients");

            //return RedirectToAction("Index", "Home");

        }




    }
}
