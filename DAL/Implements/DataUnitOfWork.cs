using DAL.DataModels;
using DAL.EntityFramework;
using DAL.Interfaces;
using DAL.Repositories;
using System.Threading;

namespace DAL.Implements
{
    public class DataUnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Client> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new GenericSafeRepository<Client>(Context, _locker);
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
                    _products = new GenericSafeRepository<Product>(Context, _locker);
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
                    _managers = new GenericSafeRepository<Manager>(Context, _locker);
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
                    _soldProducts = new GenericSafeRepository<SoldProduct>(Context, _locker);
                }
                return _soldProducts;
            }
            set => _soldProducts = value;
        }

        ApplicationContext Context { get; set; }
        ReaderWriterLockSlim _locker;
        private IGenericRepository<Client> _clients;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Manager> _managers;
        private IGenericRepository<SoldProduct> _soldProducts;

        public DataUnitOfWork(ApplicationContext context, ReaderWriterLockSlim locker = null)
        {
            Context = context;
            _locker = locker ?? new ReaderWriterLockSlim();
        }

        public void SaveChanges()
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            /*_locker.EnterReadLock();
            try
            {
                Context.SaveChanges();
            }
            finally
            {
                _locker.ExitWriteLock();
            }*/
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
