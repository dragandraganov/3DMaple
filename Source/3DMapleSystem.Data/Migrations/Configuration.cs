namespace _3DMapleSystem.Data.Migrations
{
    using _3DMapleSystem.Common;
    using _3DMapleSystem.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_3DMapleSystem.Data._3DMapleSystemDbContext>
    {
        private UserManager<User> userManager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(_3DMapleSystemDbContext context)
        {
            var allModels = context.PolyModels.ToList();
            foreach (var model in allModels)
            {
                context.PolyModels.Remove(model);
                context.SaveChanges();
            }

            if (context.Users.FirstOrDefault(u => u.Email == "yabalcho@bg.bg") == null)
            {
                this.userManager = new UserManager<User>(new UserStore<User>(context));

                if (context.Roles.FirstOrDefault(r => r.Name == GlobalConstants.AdminRole) == null)
                {
                    var adminRole = new IdentityRole(GlobalConstants.AdminRole);
                    context.Roles.Add(adminRole);
                    context.SaveChanges();
                }

                var user = new User();
                user.UserName = "yabalcho";
                user.Email = "yabalcho@bg.bg";
                this.userManager.Create(user, "6totos@mpi4");
                this.userManager.AddToRole(user.Id, GlobalConstants.AdminRole);
                context.SaveChanges();
            }

        }
    }
}
