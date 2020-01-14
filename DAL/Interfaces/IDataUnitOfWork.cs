using Core;

namespace DAL.Interfaces
{
    public interface IDataUnitOfWork
    {
        public IGenericRepository<Client> Clients { get; set; }
        public IGenericRepository<Manager> Managers { get; set; }
        public IGenericRepository<Product> Products { get; set; }
        public IGenericRepository<SoldProduct> SoldProducts { get; set; }
        void SaveChanges();
    }
}