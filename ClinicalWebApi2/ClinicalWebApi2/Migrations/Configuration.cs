namespace ClinicalWebApi2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    internal sealed class Configuration : DbMigrationsConfiguration<ClinicalWebApi2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClinicalWebApi2.Models.ApplicationDbContext context)
        {
            //var appUser = new ApplicationUser
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Email = "asim.irshad16@gmail.com",
            //    EmailConfirmed = true,
            //    PasswordHash = new PasswordHasher().HashPassword("123Pucit@"),
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    PhoneNumber = "+923244013397",
            //    PhoneNumberConfirmed = true,
            //    UserName = "asim.irshad16@gmail.com"
            //};
            //var appRole = new IdentityRole
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Admin"
            //};
            //var appRole2 = new IdentityRole
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Patient"
            //};
            //var appRole3 = new IdentityRole
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Doctor"
            //};
            //var appRole4 = new IdentityRole
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Assistant"
            //};
            //var userRole = new IdentityUserRole();
            //userRole.RoleId = appRole.Id;
            //userRole.UserId = appUser.Id;

            //appRole.Users.Add(userRole);

            //context.Users.AddOrUpdate(appUser);
            //context.Roles.AddOrUpdate(appRole);
            //context.Roles.AddOrUpdate(appRole2);
            //context.Roles.AddOrUpdate(appRole3);
            //context.Roles.AddOrUpdate(appRole4);
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
        }
    }
}
