using GuildCars.Data.Factories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = SpecialRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }

        public ActionResult Specials()
        {
            var model = SpecialRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }

        public ActionResult Contact()
        {
            ContactViewModel model = new ContactViewModel();
            return View(model);
        }

        public ActionResult ContactViaDetail(int id)
        {

            var model = new ContactViewModel();
            var contact = new Contact();
            var repoVehicle = VehicleRepositoryFactory.GetRepository();
            model.Vehicle = repoVehicle.GetById(id);
            model.Message = "Vin: " + model.Vehicle.Vin;

            model.Contact = contact;
            model.Contact.Message = model.Message;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var repo = ContactRepositoryFactory.GetRepository();

                repo.Add(contact);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}