using DAL.DataModels;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Client> Clients { get; set; }
        public IGenericRepository<Manager> Managers { get; set; }
        public IGenericRepository<Product> Products { get; set; }
        public IGenericRepository<SoldProduct> SoldProducts { get; set; }

        void SaveChanges();
    }
}