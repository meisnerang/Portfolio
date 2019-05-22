using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class UsersWithRolesViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}