using System;
using System.Linq;
using System.Linq.Expressions;

namespace _3DMapleSystem.Data.Common.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All(params Expression<Func<T, object>>[] includeExpressions);

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);

        int SaveChanges();
    }
}
