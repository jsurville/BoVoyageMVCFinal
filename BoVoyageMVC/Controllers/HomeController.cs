using BoVoyageMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace BoVoyageMVC.Controllers
{
    public class HomeController : BaseController
    {
       
        public ActionResult Index()
        {
            var voyage= db.Voyages.Include("Destination").Include(x => x.Destination.Images).ToList();
            return View(voyage);
        }
        [Route("A-propos")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}