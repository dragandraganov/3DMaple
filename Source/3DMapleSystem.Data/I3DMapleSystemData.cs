using _3DMapleSystem.Data.Common.Repository;
using _3DMapleSystem.Data.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace _3DMapleSystem.Data
{
    public interface I3DMapleSystemData
    {
        DbContext Context { get; }

        IDeletableEntityRepository<User> Users { get; }

        IDeletableEntityRepository<Category> Categories { get; }

        IDeletableEntityRepository<SubCategory> SubCategories { get; }

        IDeletableEntityRepository<PolyModel> PolyModels { get; }

        IDeletableEntityRepository<AppFile> AppFiles { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Platform> Platforms { get; }

        IDeletableEntityRepository<SubPlatform> SubPlatforms { get; }

        IDeletableEntityRepository<Style> Styles { get; }

        IDeletableEntityRepository<Tag> Tags { get; }

        void Dispose();

        int SaveChanges();
    }
}
