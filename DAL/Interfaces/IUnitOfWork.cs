using DAL.DataModels;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Client> Clients { get; set; }
        IGenericRepository<Manager> Managers { get; set; }
        IGenericRepository<Product> Products { get; set; }
        IGenericRepository<SoldProduct> SoldProducts { get; set; }

        void SaveChanges();
    }
}