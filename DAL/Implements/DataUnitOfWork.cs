using DAL.DataModels;
using DAL.EntityFramework;
using DAL.Interfaces;
using DAL.Repositories;
using System.Threading;

namespace DAL.Implements
{
    public class DataUnitOfWork : IUnitOfWork
    {
        private IGenericRepository<Client> _clients;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Manager> _managers;
        private IGenericRepository<SoldProduct> _soldProducts;

        public IGenericRepository<Client> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new GenericRepository<Client>(Context);
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
                    _products = new GenericRepository<Product>(Context);
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
                    _managers = new GenericRepository<Manager>(Context);
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
                    _soldProducts = new GenericRepository<SoldProduct>(Context);
                }
                return _soldProducts;
            }
            set => _soldProducts = value;
        }

        ApplicationContext Context { get; set; }

        public DataUnitOfWork(ApplicationContext context)
        {
            Context = context;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
