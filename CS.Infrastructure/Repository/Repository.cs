using CS.Infrastructure.Context;
using CS.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CS.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CartContext _dbContext;

        public Repository(CartContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(long id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IQueryable<T> List()
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public T GetLastInserted()
        {
            return _dbContext.Set<T>().Last();
        }
    }
}
