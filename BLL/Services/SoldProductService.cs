using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<SoldProductDTO> Search(int price)
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
                items = items.Where(i => i.Product.Price > price);
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
    }
}
