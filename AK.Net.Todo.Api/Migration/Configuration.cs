namespace AK.Net.Todo.Api.Migration
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migration";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Anjaiah",
                Email = "anjaiah.keesari@axa-ppp.co.uk",
                EmailConfirmed = true,
                //FirstName = "Anjaiah",
                //LastName = "Keesari",
                //Level = 1,
                //JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "anjaiah.keesari@axa-ppp.co.uk");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Client" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("Anjaiah");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Client", "User" });
        }
    }
}
