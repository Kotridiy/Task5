using Core;
using DAL.DataModels;
using DAL.Interfaces;
using DAL.Repositories;
using System;

namespace DAL.Implements
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public IGenericRepository<Client> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new GenericMockRepository<Client>();
                    if (!_isClientCreated)
                    {
                        _clients.Add(new Client() { Id = 1, Name = "Даша Мартыненко", Birthday = new DateTime(1997, 3, 10), Gender = Gender.Female });
                        _clients.Add(new Client() { Id = 2, Name = "Егор Земов", Birthday = new DateTime(1985, 4, 15), Gender = Gender.Male });
                        _clients.Add(new Client() { Id = 3, Name = "Димка Димка", Birthday = new DateTime(2005, 7, 1), Gender = Gender.Male });
                        _clients.Add(new Client() { Id = 4, Name = "Пушок", Birthday = DateTime.Today, Gender = Gender.Undetermined });
                        _isClientCreated = true;
                    }
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
                    _products = new GenericMockRepository<Product>();
                    if (!_isProductCreated)
                    {
                        _products.Add(new Product() { Id = 1, Name = "Вилы", Count = 5, Price = 23 });
                        _products.Add(new Product() { Id = 2, Name = "Грабли", Count = 13, Price = 15 });
                        _products.Add(new Product() { Id = 3, Name = "Галоши", Count = 20, Price = 20 });
                        _isProductCreated = true;
                    }
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
                    _managers = new GenericMockRepository<Manager>();
                    if (!_isManagerCreated)
                    {
                        _managers.Add(new Manager() { Id = 1, Name = "Иван Требушко", Age = 23, Salary = 1006, Gender = Gender.Male });
                        _managers.Add(new Manager() { Id = 2, Name = "Вероника Ковальчук", Age = 22, Salary = 1765, Gender = Gender.Female });
                        _isManagerCreated = true;
                    }
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
                    _soldProducts = new GenericMockRepository<SoldProduct>();
                }
                return _soldProducts;
            }
            set => _soldProducts = value;
        }

        private IGenericRepository<Client> _clients;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Manager> _managers;
        private IGenericRepository<SoldProduct> _soldProducts;
        private static bool _isClientCreated; 
        private static bool _isProductCreated; 
        private static bool _isManagerCreated; 

        public void Dispose() { }
        public void SaveChanges() { }
    }
}
