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
    class ClientRepository : IGenericRepository<Client>
    {
        ApplicationContext Context { get; set; }
        ReaderWriterLockSlim _locker;

        public ClientRepository(ApplicationContext context, ReaderWriterLockSlim locker)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _locker = locker;
        }

        public IEnumerable<Client> GetAll()
        {
            _locker.EnterReadLock();
            IEnumerable<Client> models;
            try
            {
                models = Context.Clients.AsEnumerable();
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return models;
        }

        public Client Get(int id)
        {
            _locker.EnterReadLock();
            Client model;
            try
            {
                model = Context.Clients.Find(id);
            }
            finally
            {
                _locker.ExitReadLock();
            }
            return model;
        }

        public void Add(Client entity)
        {
            Context.Add(entity);
        }

        public void Update(Client entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Client entity)
        {
            Context.Remove(entity);
        }
    }
}
