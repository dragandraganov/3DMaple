namespace _3DMapleSystem.Data.Migrations
{
    using System.Web;
    using _3DMapleSystem.Common;
    using _3DMapleSystem.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
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

            //Add manager
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

            //Create Default Image
            if (context.AppFiles.FirstOrDefault(f=>f.NameInDb=="Default")==null)
            {
                this.AddDefaultImage(context);
            }
        }

        private void AddDefaultImage(_3DMapleSystemDbContext context)
        {
            using (var memory = new MemoryStream())
            {
                string path = HttpContext.Current.Server.MapPath(GlobalConstants.PathDefaultImage);

                using (var file = new FileStream(path, FileMode.Open))
                {
                    file.CopyTo(memory);
                    var content = memory.GetBuffer();
                    var defaultImage = new AppFile
                    {
                        Content = content,
                        FileExtension = "png",
                        NameInDb="Default"
                    };

                    context.AppFiles.Add(defaultImage);
                    context.SaveChanges();
                }
            }
        }
    }
}
