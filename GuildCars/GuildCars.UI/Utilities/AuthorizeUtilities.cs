using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Utilities
{
    public class AuthorizeUtilities
    {
        public static string GetUserId(Controller controller)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userMgr.FindByName(controller.User.Identity.Name);
            return user.Id;
        }

        public static ApplicationUser GetUserByUserName(string userName)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userMgr.FindByName(userName);
            return user;
        }

        
        public static List<ApplicationUser> GetAllUsers()
        {
            var context = new ApplicationDbContext();
            var allUsers = context.Users.ToList();
            return allUsers;
        }

        public static List<IdentityRole> GetAllRoles()
        {
            var context = new ApplicationDbContext();
            var allRoles = context.Roles.ToList();
            return allRoles;
        }
                
    }
}