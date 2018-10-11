using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BoVoyageMVC.Models;
using BoVoyageMVC.Tools;
using BoVoyageMVC.Controllers;

namespace BoVoyageMVC.Areas.BackOffice.Controllers
{
    [Authorize(Roles = "Commercial")]
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "PAGE ADMIN";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Numéro Contact Agence";

            return View();
        }
       

    }
}