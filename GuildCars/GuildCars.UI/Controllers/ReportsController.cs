using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        // GET: Reports
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            return View();
        }

        public ActionResult Sales()
        {
            //use Admin/Users for template

            

            return View();
        }
    }
}