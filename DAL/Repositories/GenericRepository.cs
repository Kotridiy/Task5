using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL.Repositories
{
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DbContext Context { get; set; }

        public GenericRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>().AsQueryable(); ;
        }

        public T Get(int id)
        {
            return Context.Find(typeof(T), id) as T; ;
        }

        public void Add(T entity)
        {
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }
    }
}