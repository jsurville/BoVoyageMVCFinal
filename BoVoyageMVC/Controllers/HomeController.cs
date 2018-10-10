using BoVoyageMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageMVC.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Voyages.Include("Destinations"));
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