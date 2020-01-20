using DAL.DataModels;
using DAL.EntityFramework;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DAL.Repositories
{
    class ProductRepository : IGenericRepository<Product>
    {
        ApplicationContext Context { get; set; }
        ReaderWriterLockSlim _locker;

        public ProductRepository(ApplicationContext context, ReaderWriterLockSlim locker)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _locker = locker;
        }

        public IEnumerable<Product> GetAll()
        {
            _locker.EnterReadLock();
            IEnumerable<Product> models;
            try
            {
                models = Context.Products.AsEnumerable();
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return models;
        }

        public Product Get(int id)
        {
            _locker.EnterReadLock();
            Product model;
            try
            {
                model = Context.Products.Find(id);
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return model;
        }

        public void Add(Product entity)
        {
            Context.Add(entity);
        }

        public void Update(Product entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Product entity)
        {
            Context.Remove(entity);
        }
    }
}
