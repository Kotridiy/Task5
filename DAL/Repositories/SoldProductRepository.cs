using Core;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DAL.Repositories
{
    class SoldProductRepository : IGenericRepository<SoldProduct>
    {
        ApplicationContext Context { get; set; }
        ReaderWriterLockSlim _locker;

        public SoldProductRepository(ApplicationContext context, ReaderWriterLockSlim locker)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _locker = locker;
        }

        public IEnumerable<SoldProduct> GetAll()
        {
            _locker.EnterReadLock();
            IEnumerable<SoldProduct> models;
            try
            {
                models = Context.SoldProducts.AsEnumerable();
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return models;
        }

        public SoldProduct Get(int id)
        {
            _locker.EnterReadLock();
            SoldProduct model;
            try
            {
                model = Context.SoldProducts.Find(id);
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return model;
        }

        public void Add(SoldProduct entity)
        {
            Context.Add(entity);
        }

        public void Update(SoldProduct entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(SoldProduct entity)
        {
            Context.Remove(entity);
        }
    }
}
