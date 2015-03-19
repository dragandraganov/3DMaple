using _3DMapleSystem.Data.Common.Models;
using _3DMapleSystem.Data.Common.Repository;
using _3DMapleSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace _3DMapleSystem.Data
{
    public class _3DMapleSystemData : I3DMapleSystemData
    {
        private readonly DbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public _3DMapleSystemData(DbContext context)
        {
            this.context = context;
        }

        public DbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IDeletableEntityRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IDeletableEntityRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IDeletableEntityRepository<SubCategory> SubCategories
        {
            get
            {
                return this.GetRepository<SubCategory>();
            }
        }

        public IDeletableEntityRepository<PolyModel> PolyModels
        {
            get
            {
                return this.GetRepository<PolyModel>();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
