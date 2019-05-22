using GuildCars.Data.Factories;
using GuildCars.UI.Models;
using GuildCars.Models.Tables;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GuildCars.UI.Utilities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GuildCars.UI.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        //do I need a RoleManager?
        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //GET
        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            var context = new ApplicationDbContext();

            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UsersWithRolesViewModel()

                                  {
                                      UserId = p.UserId,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }

        //GET
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            var model = new UserAddViewModel();

            var allRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList(); ;

            ViewBag.Roles = allRoles;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(UserAddViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.UserRole);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("EditUser", new { id = user.Id });
                }
                else
                {
                    var allRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList().Select(rr =>

                            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

                    ViewBag.Roles = allRoles;
                    AddErrors(result);
                }
            }
 
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //GET
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string id)
        {
            var model = new UserEditViewModel();

            model.User = UserManager.FindById(id);
            model.FirstName = model.User.FirstName;
            model.LastName = model.User.LastName;
            model.Email = model.User.Email;
            model.Password = model.User.PasswordHash;
            model.UserRole = UserManager.GetRoles(id).FirstOrDefault();
            
            var allRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name, Selected = model.UserRole == rr.Name }).ToList(); ;
            ViewBag.Roles = allRoles;

            return View(model);
        }

        //POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditUser(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var allRoles = (new ApplicationDbContext()).Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name, Selected = model.UserRole == rr.Name }).ToList(); ;
                ViewBag.Roles = allRoles;

                return View(model);
            }
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(store);
            var currentUser = manager.FindById(model.User.Id);
            currentUser.FirstName = model.FirstName;
            currentUser.LastName = model.LastName;
            currentUser.Email = model.Email;
            currentUser.PasswordHash = model.Password;
            
            string[] allUserRoles = UserManager.GetRoles(currentUser.Id).ToArray();
            UserManager.RemoveFromRoles(currentUser.Id, allUserRoles);

            await UserManager.AddToRoleAsync(currentUser.Id, model.UserRole);

            if (!String.IsNullOrEmpty(model.Password) && String.IsNullOrEmpty(model.ConfirmPassword))
            {
                ModelState.AddModelError("PasswordConfirm", "Please confirm the new password you entered.");
            }

            await manager.UpdateAsync(currentUser);
            var ctx = store.Context;
            ctx.SaveChanges();

            TempData["Message"] = "Profile Changes Saved !";
            return RedirectToAction("Users");
        }

        //GET: AdminVehicles
        [Authorize(Roles = "Admin")]
        public ActionResult Vehicles()
        {
            var model = VehicleRepositoryFactory.GetRepository().GetAll();
            return View();
        }

        //GET
        [Authorize(Roles = "Admin")]
        public ActionResult AddVehicle()
        {
            var model = new VehicleAddViewModel();

            var makeRepo = MakeRepositoryFactory.GetRepository();
            var modelRepo = ModelRepositoryFactory.GetRepository();
            var bodyStyleRepo = BodyStyleRepositoryFactory.GetRepository();
            var colorRepo = ColorRepositoryFactory.GetRepository();
            var interiorRepo = ColorRepositoryFactory.GetRepository();


            model.Make = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
            model.Model = new SelectList(modelRepo.GetAll(), "ModelId", "ModelName");
            model.BodyStyle = new SelectList(bodyStyleRepo.GetAll(), "BodyStyleId", "BodyStyleName");
            model.Color = new SelectList(colorRepo.GetAll(), "ColorId", "ColorName");
            model.Interior = new SelectList(interiorRepo.GetAll(), "ColorId", "ColorName");

            model.Vehicle = new Vehicle();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddVehicle(VehicleAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepositoryFactory.GetRepository();

                try
                {
                    model.Vehicle.UserId = AuthorizeUtilities.GetUserId(this);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        //re-name uploaded photo file
                        string inventoryNameBase = "inventory-";
                        string carId = (repo.GetMaxVehicleId() + 1).ToString();
                        
                        string newFileName = inventoryNameBase + carId;

                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, newFileName + extension);

                        model.ImageUpload.SaveAs(filePath);
                        model.Vehicle.PhotoFile = Path.GetFileName(filePath);
                    }

                    repo.Add(model.Vehicle);
                    
                    return RedirectToAction("EditVehicle", new { id = model.Vehicle.VehicleId });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Admin
        public ActionResult Specials()
        {
            Special model = new Special();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddSpecial(Special special)
        {
            if (ModelState.IsValid)
            {
                var repo = SpecialRepositoryFactory.GetRepository();

                repo.Add(special);

                return RedirectToAction("Specials", "Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteSpecial(int specialId)
        {
            var repo = SpecialRepositoryFactory.GetRepository();
            repo.Delete(specialId);

            return RedirectToAction("Specials", "Admin");
        }

        //GET
        [Authorize(Roles = "Admin")]
        public ActionResult EditVehicle(int id)
        {
            var model = new VehicleEditViewModel();

            var makeRepo = MakeRepositoryFactory.GetRepository();
            var modelRepo = ModelRepositoryFactory.GetRepository();
            var bodyStyleRepo = BodyStyleRepositoryFactory.GetRepository();
            var colorRepo = ColorRepositoryFactory.GetRepository();
            var interiorRepo = ColorRepositoryFactory.GetRepository();
            var vehicleRepo = VehicleRepositoryFactory.GetRepository();


            model.Make = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
            model.Model = new SelectList(modelRepo.GetAll(), "ModelId", "ModelName");
            model.BodyStyle = new SelectList(bodyStyleRepo.GetAll(), "BodyStyleId", "BodyStyleName");
            model.Color = new SelectList(colorRepo.GetAll(), "ColorId", "ColorName");
            model.Interior = new SelectList(interiorRepo.GetAll(), "ColorId", "ColorName");
            
            model.Vehicle = vehicleRepo.GetByIdByVehicle(id);
            //model.VehicleMSRP = model.Vehicle.MSRP;
            
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditVehicle(VehicleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepositoryFactory.GetRepository();

                try
                {
                    model.Vehicle.UserId = AuthorizeUtilities.GetUserId(this);
                    var oldVehicle = repo.GetById(model.Vehicle.VehicleId);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        //re-name uploaded photo file
                        string inventoryNameBase = "inventory-";
                        string carId = (repo.GetMaxVehicleId() + 1).ToString();

                        string newFileName = inventoryNameBase + carId;

                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, newFileName + extension);

                        model.ImageUpload.SaveAs(filePath);
                        model.Vehicle.PhotoFile = Path.GetFileName(filePath);

                        // delete old file
                        var oldPath = Path.Combine(savepath, oldVehicle.PhotoFile);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    else
                    {
                        // they did not replace the old file, so keep the old file name
                        model.Vehicle.PhotoFile = oldVehicle.PhotoFile;
                    }

                    repo.Edit(model.Vehicle);
                    return RedirectToAction("EditVehicle", new { id = model.Vehicle.VehicleId });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var makeRepo = MakeRepositoryFactory.GetRepository();
                var modelRepo = ModelRepositoryFactory.GetRepository();
                var bodyStyleRepo = BodyStyleRepositoryFactory.GetRepository();
                var colorRepo = ColorRepositoryFactory.GetRepository();
                var interiorRepo = ColorRepositoryFactory.GetRepository();

                model.Make = new SelectList(makeRepo.GetAll(), "MakeId", "MakeName");
                model.Model = new SelectList(modelRepo.GetAll(), "ModelId", "ModelName");
                model.BodyStyle = new SelectList(bodyStyleRepo.GetAll(), "BodyStyleId", "BodyStyleName");
                model.Color = new SelectList(colorRepo.GetAll(), "ColorId", "ColorName");
                model.Interior = new SelectList(interiorRepo.GetAll(), "ColorId", "ColorName");

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult DeleteVehicle(VehicleEditViewModel model)
        {
           var repo = VehicleRepositoryFactory.GetRepository();

            //delete from Sale table first
           repo.DeleteSaleVehicle(model.Vehicle.VehicleId);
           repo.Delete(model.Vehicle.VehicleId);

            return RedirectToAction("Vehicles");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Makes()
        {
            Make model = new Make();
            ViewBag.CurrentDateTime = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMake(Make make)
        {
            if (ModelState.IsValid)
            {
                var repo = MakeRepositoryFactory.GetRepository();

                make.UserId = AuthorizeUtilities.GetUserId(this);
                repo.Add(make);

                return RedirectToAction("Makes", "Admin");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Models()
        {
            var model = new Model();
            
            ViewBag.CurrentDateTime = DateTime.Now;

            var makesRepo = MakeRepositoryFactory.GetRepository();

            ViewBag.Makes = new SelectList(makesRepo.GetAll(), "MakeID", "MakeName");

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddModel(Model model)
        {
            if (ModelState.IsValid)
            {
                var repo = ModelRepositoryFactory.GetRepository();
                model.UserId = AuthorizeUtilities.GetUserId(this);
                
                repo.Add(model);

                return RedirectToAction("Models", "Admin");
            }
            else
            {
                return View();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}