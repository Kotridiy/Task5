using Core;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Threading;

namespace DAL
{
    class DataUnitOfWork : IDataUnitOfWork, IDisposable
    {
        public IGenericRepository<Client> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new ClientRepository(Context, _locker);
                }
                return _clients;
            }
            set => _clients = value;
        }

        public IGenericRepository<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new ProductRepository(Context, _locker);
                }
                return _products;
            }
            set => _products = value;
        }

        public IGenericRepository<Manager> Managers
        {
            get
            {
                if (_managers == null)
                {
                    _managers = new ManagerRepository(Context, _locker);
                }
                return _managers;
            }
            set => _managers = value;
        }

        public IGenericRepository<SoldProduct> SoldProducts
        {
            get
            {
                if (_soldProducts == null)
                {
                    _soldProducts = new SoldProductRepository(Context, _locker);
                }
                return _soldProducts;
            }
            set => _soldProducts = value;
        }

        ApplicationContext Context { get; set; }
        ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
        private IGenericRepository<Client> _clients;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Manager> _managers;
        private IGenericRepository<SoldProduct> _soldProducts;

        public DataUnitOfWork(ApplicationContext context)
        {
            Context = context;
        }

        public void SaveChanges()
        {
            _locker.EnterReadLock();
            try
            {
                Context.SaveChanges();
            }
            finally
            {
                _locker.ExitWriteLock();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
