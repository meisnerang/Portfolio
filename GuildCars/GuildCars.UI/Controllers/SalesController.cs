using GuildCars.Data.Factories;
using GuildCars.UI.Models;
using GuildCars.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        [Authorize(Roles = "Sales")]
        public ActionResult Index()
        {
            var model = VehicleRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }

        //GET: Purchase
        [Authorize(Roles = "Sales")]
        public ActionResult Purchase(int id)
        {

            var model = new VehiclePurchaseViewModel();

            var statesRepo = StatesRepositoryFactory.GetRepository();
            var purchaseTypeRepo = PurchaseTypeRepositoryFactory.GetRepository();
            var repo = VehicleRepositoryFactory.GetRepository();

            model.State = new SelectList(statesRepo.GetAll(), "StateId", "StateId");
            model.PurchaseType = new SelectList(purchaseTypeRepo.GetAll(), "PurchaseTypeId", "PurchaseTypeName");
            model.VehicleItem = repo.GetById(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Sales")]
        public ActionResult Purchase(VehiclePurchaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = SalesRepositoryFactory.GetRepository();
                model.SaleItem.VehicleId = model.VehicleItem.VehicleId;
                model.SaleItem.UserId = AuthorizeUtilities.GetUserId(this);
                //model.SaleItem.UserId = "00000000-0000-0000-0000-000000000000";
                repo.Add(model.SaleItem);

                return RedirectToAction("Index");
            }
            else
            {
                var statesRepo = StatesRepositoryFactory.GetRepository();
                var purchaseTypeRepo = PurchaseTypeRepositoryFactory.GetRepository();

                model.State = new SelectList(statesRepo.GetAll(), "StateId", "StateId");
                model.PurchaseType = new SelectList(purchaseTypeRepo.GetAll(), "PurchaseTypeId", "PurchaseTypeName");

                return View(model);
            }

        }
    }
}