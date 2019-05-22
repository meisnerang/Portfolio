using GuildCars.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: NewInventory
        public ActionResult New()
        {
            var model = VehicleRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }

        //GET: UsedInventory
        public ActionResult Used()
        {
            var model = VehicleRepositoryFactory.GetRepository().GetAll();
            return View();
        }

        //GET: Details
        public ActionResult Details(int id)
        {
            var model = VehicleRepositoryFactory.GetRepository().GetById(id);
            return View(model);
        }
    }
}