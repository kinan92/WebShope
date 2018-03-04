namespace WebShope.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebShope.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebShope.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebShope.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (roleManager.FindByName("IamTheAdmin") ==null)
            {
                roleManager.Create(new IdentityRole("IamTheAdmin"));
            }
            if (roleManager.FindByName("NormalUser") ==null)
            {
                roleManager.Create(new IdentityRole("NormalUser"));
            }

            ApplicationUser IamAnAdmin;
            ApplicationUser IamAnCommen;

            if (userManager.FindByName("KinanTheAdmin")==null)
            {
                IamAnAdmin = new ApplicationUser()
                {
                    Email = "Kinan.Karam@Gmail.com",
                    UserName = "KinanTheAdmin",
                    FirstName = "Kinan",
                    LastName = "Karam",
                    Age = 25,
                    PhoneNumber = "0729026684",
                    Adress = "Karlskrona",
                };
                userManager.Create(IamAnAdmin, ("!23Qwe"));
            }

            if (context.Users.SingleOrDefault(x=>x.Email== "Normal.User@Gmail.com") ==null)
            {
                IamAnCommen = new ApplicationUser()
                {
                    Email = "Normal.User@Gmail.com",
                    UserName = "NormalUser",
                    FirstName = "Normal",
                    LastName = "normalUser",
                    Age = 44,
                    PhoneNumber = "0000000000",
                    Adress = "Växjö",
                };
                userManager.Create(IamAnCommen, "!23Qwe");
            }

            context.SaveChanges();

            IamAnAdmin = userManager.FindByName("KinanTheAdmin");
            IamAnCommen = userManager.FindByEmail("Normal.User@Gmail.com");

            userManager.AddToRole(IamAnAdmin.Id,"IamTheAdmin");
            userManager.AddToRole(IamAnCommen.Id, "NormalUser");
        }
    }
}
