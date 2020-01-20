using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DAL.Repositories
{
    class GenericSafeRepository<T> : IGenericRepository<T> where T : class
    {
        DbContext Context { get; set; }
        ReaderWriterLockSlim _locker;

        public GenericSafeRepository(DbContext context, ReaderWriterLockSlim locker)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _locker = locker;
        }

        public IEnumerable<T> GetAll()
        {
            _locker.EnterReadLock();
            IEnumerable<T> models;
            try
            {
                models = Context.Set<T>().AsEnumerable();
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return models;
        }

        public T Get(int id)
        {
            _locker.EnterReadLock();
            T model;
            try
            {
                model = Context.Find(typeof(T), id) as T;
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return model;
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
