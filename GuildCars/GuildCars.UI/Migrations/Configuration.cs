namespace GuildCars.UI.Migrations
{
    using GuildCars.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.UI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            CreateRoles(context);
        }

        private void CreateRoles(ApplicationDbContext context)
        {
            var userMgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // creating Admin & Sales roles 
            if (!roleMgr.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleMgr.Create(role);
            }
    
            if (!roleMgr.RoleExists("Sales"))
            {
                var role = new IdentityRole();
                role.Name = "Sales";
                roleMgr.Create(role);
            }

            if(!roleMgr.RoleExists("Disabled"))
            {
                var role = new IdentityRole();
                role.Name = "Disabled";
                roleMgr.Create(role);
            }
        }
    }
}
