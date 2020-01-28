using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using DAL;
using DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class SoldProductService
    {
        public string _connectionInfo;

        public SoldProductService(string connectionInfo)
        {
            _connectionInfo = connectionInfo;
        }

        public IEnumerable<SoldProductDTO> GetAll()
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<SoldProduct, SoldProductDTO>();
                        cfg.CreateMap<Product, ProductDTO>();
                        cfg.CreateMap<Client, ClientDTO>();
                        cfg.CreateMap<Manager, ManagerDTO>();
                    });
                var items = unitOfWork.SoldProducts.GetAll();
                try
                {
                    return mapConfig.CreateMapper().Map<IEnumerable<SoldProductDTO>>(items);
                }
                catch
                {

                    throw new DatabaseException();
                }
            }
        }

        public SoldProductDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProduct, SoldProductDTO>());
                SoldProduct item;
                try
                {
                    item = unitOfWork.SoldProducts.Get(id);
                }
                catch
                {
                    throw new DatabaseException();
                }
                return mapConfig.CreateMapper().Map<SoldProductDTO>(item);
            }
        }

        public void Add(SoldProductDTO itemDTO, int clientId, int productId)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductDTO, SoldProduct>());
                var item = mapConfig.CreateMapper().Map<SoldProduct>(itemDTO);

                Product product;
                try
                {
                    item.Client = unitOfWork.Clients.Get(clientId);
                    item.Manager = unitOfWork.Managers.Get(1);
                    product = unitOfWork.Products.Get(productId);
                }
                catch
                {
                    throw new DatabaseException();
                }

                if (product.Count == 0)
                {
                    throw new ValidationException("This product sold out!");
                }
                else
                {
                    product.Count--;
                    unitOfWork.Products.Update(product);
                    item.Product = product;
                }

                unitOfWork.SoldProducts.Add(item);
                unitOfWork.SaveChanges();
            }
        }

        public (IEnumerable<ClientDTO>, IEnumerable<ProductDTO>) GetData()
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var clientMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>());
                var productMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
                var clients = unitOfWork.Clients.GetAll();
                var products = unitOfWork.Products.GetAll();
                IEnumerable<ClientDTO> clientDtos;
                IEnumerable<ProductDTO> productDtos;
                try
                {
                    clientDtos = clientMapConfig.CreateMapper().Map<IEnumerable<ClientDTO>>(clients);
                    productDtos = productMapConfig.CreateMapper().Map<IEnumerable<ProductDTO>>(products);
                }
                catch
                {
                    throw new DatabaseException();
                }

                return (clientDtos, productDtos);
            }
        }

        public IEnumerable<SoldProductDTO> Search(string priceStr, string manager, string client, string mode)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<SoldProduct, SoldProductDTO>();
                        cfg.CreateMap<Product, ProductDTO>();
                        cfg.CreateMap<Client, ClientDTO>();
                        cfg.CreateMap<Manager, ManagerDTO>();
                    });
                var items = unitOfWork.SoldProducts.GetAll();
                decimal price;
                try
                {
                    price = priceStr != "" ? decimal.Parse(priceStr) : 0;
                }
                catch (FormatException)
                {
                    throw new ValidationException("price is not decimal.");
                }
                bool modeAnd = mode == "and";
                items = Filter(items, price, manager, client, modeAnd);
                try
                {
                    return mapConfig.CreateMapper().Map<IEnumerable<SoldProductDTO>>(items);
                }
                catch
                {
                    throw new DatabaseException();
                }
            }
        }

        private IQueryable<SoldProduct> Filter(IQueryable<SoldProduct> items, decimal price, string manager, string client, bool modeAnd)
        {
            var query = items;
            if (modeAnd)
            {
                if (price != 0)
                {
                    query = query.Where(item => item.Product.Price > price);
                }
                if (manager != "")
                {
                    query = query.Where(item => item.Manager.Name == manager);
                }
                if (client != "")
                {
                    query = query.Where(item => item.Client.Name == client);
                }
            }
            else
            {
                query = query.Where(item => item.Product.Price > price || item.Manager.Name == manager || item.Client.Name == client);
            }
            return query;
        }
    }
}
