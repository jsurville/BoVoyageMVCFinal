using BoVoyageMVC.Controllers;
using BoVoyageMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [RouteArea("BackOffice")]
    [Authorize(Roles = "Commercial,Admin")]
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


        //public ActionResult Edit(int id)
        //{
        //    if (id == 0)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Client client = db.Clients.Find(id);
        //    if (client == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(client);

        //}

        public ActionResult Search(string search, string dateDebut, string dateFin)
        {
            IEnumerable<Client> clients = db.Clients;
            DateTime avantNaissance;
            DateTime apresNaissance;
            if (!string.IsNullOrWhiteSpace(search))
            {
                clients = clients.Where(x => x.LastName.Contains(search)
                || x.FisrtName.Contains(search) || x.Address.Contains(search)
                || x.Address.Contains(search));
            }

            if (DateTime.TryParse(dateDebut, out avantNaissance))
                clients = clients.Where(c => c.BirthDate > avantNaissance);

            if (DateTime.TryParse(dateFin, out apresNaissance))
                clients = clients.Where(c =>c.BirthDate < apresNaissance);

            if (clients?.Count() == 0)
            {
                Display("Aucun Résultat ");              
            }           

            return View("Index", clients.ToList());
        }

        public ActionResult Download()
        {
            ICollection<Client> clients = db.Clients.ToList();

            string csv = ListToCSV(clients);

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "clients.csv");
        }

        private string ListToCSV<T>(IEnumerable<T> list)
        {
            StringBuilder sList = new StringBuilder();

            Type type = typeof(T);
            var props = type.GetProperties();
            sList.Append(string.Join(",", props.Select(p => p.Name)));
            sList.Append(Environment.NewLine);

            foreach (var element in list)
            {
                sList.Append(string.Join(",", props.Select(p => p.GetValue(element, null))));
                sList.Append(Environment.NewLine);
            }

            return sList.ToString();
        }
    }
}
