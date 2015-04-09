using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Data.Common.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using _3DMapleSystem.Data.Migrations;

namespace _3DMapleSystem.Data
{
    public class _3DMapleSystemDbContext : IdentityDbContext<User>
    {
        public _3DMapleSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<_3DMapleSystemDbContext, Configuration>());
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<SubCategory> SubCategories { get; set; }

        public virtual IDbSet<PolyModel> PolyModels { get; set; }

        public virtual IDbSet<AppFile> AppFiles { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Platform> Platforms { get; set; }

        public virtual IDbSet<SubPlatform> SubPlatforms { get; set; }

        public virtual IDbSet<Style> Styles { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<ModelRank> ModelRanks { get; set; }

        public virtual IDbSet<DownloadedPolyModelsUsers> ModelsUsers { get; set; }

        public static _3DMapleSystemDbContext Create()
        {
            return new _3DMapleSystemDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }


        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            //var entries= this.ChangeTracker.Entries()
            //        .Where(
            //            e =>
            //            e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                        entity.PreserveCreatedOn = true;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
