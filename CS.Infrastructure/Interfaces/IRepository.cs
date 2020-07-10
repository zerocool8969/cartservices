using System;
using System.Linq;
using System.Linq.Expressions;

namespace CS.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(long id);
        IQueryable<T> List();
        IQueryable<T> List(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetLastInserted();
    }
}
