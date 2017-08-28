using AK.Net.Todo.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AK.Net.Todo.Api.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "Anjaiah",
                Email = "keesari_anjaiah@yahoo.co.in",
                EmailConfirmed = true,
                //FirstName = "Anjaiah",
                //LastName = "Keesari",
                //Level = 1,
                //JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "keesari_anjaiah@yahoo.co.in");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "admin" });
                roleManager.Create(new IdentityRole { Name = "client" });
                roleManager.Create(new IdentityRole { Name = "user" });
            }

            var adminUser = manager.FindByName("Anjaiah");

            manager.AddToRoles(adminUser.Id, new string[] { "admin", "client", "user" });
        }
    }
}
