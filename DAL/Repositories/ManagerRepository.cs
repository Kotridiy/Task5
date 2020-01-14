using DAL.Interfaces;
using DAL;
using System;
using System.Linq;
using System.Threading;
using Core;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    class ManagerRepository : IGenericRepository<Manager>
    {
        ApplicationContext Context { get; set; }
        ReaderWriterLockSlim _locker;

        public ManagerRepository(ApplicationContext context, ReaderWriterLockSlim locker)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _locker = locker;
        }

        public IEnumerable<Manager> GetAll()
        {
            _locker.EnterReadLock();
            IEnumerable<Manager> models;
            try
            {
                models = Context.Managers.AsEnumerable();
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return models;
        }

        public Manager Get(int id)
        {
            _locker.EnterReadLock();
            Manager model;
            try
            {
                model = Context.Managers.Find(id);
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return model;
        }

        public void Add(Manager entity)
        {
            Context.Add(entity);
        }

        public void Update(Manager entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Manager entity)
        {
            Context.Remove(entity);
        }
    }
}
