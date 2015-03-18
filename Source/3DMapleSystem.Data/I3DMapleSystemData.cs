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

        void Dispose();

        int SaveChanges();
    }
}
