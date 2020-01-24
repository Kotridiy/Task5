using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;

namespace BLL.Services
{
    public class SoldProductService
    {
        string _connectionInfo;

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
                return mapConfig.CreateMapper().Map<IEnumerable<SoldProductDTO>>(items);
            }
        }

        public SoldProductDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProduct, SoldProductDTO>());
                var item = unitOfWork.SoldProducts.Get(id);
                return mapConfig.CreateMapper().Map<SoldProductDTO>(item);
            }
        }

        public void Add(SoldProductDTO itemDTO, int clientId, int productId)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductDTO, SoldProduct>());
                var item = mapConfig.CreateMapper().Map<SoldProduct>(itemDTO);
                item.Client = unitOfWork.Clients.Get(clientId);
                item.Product = unitOfWork.Products.Get(productId);
                item.Manager = unitOfWork.Managers.Get(1);
                unitOfWork.SoldProducts.Add(item);
                unitOfWork.SaveChanges();
            }
        }

        public (IEnumerable<ClientDTO>, IEnumerable<ProductDTO>) GetData()
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var clientMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>());
                var clients = unitOfWork.Clients.GetAll();
                var clientDtos = clientMapConfig.CreateMapper().Map<IEnumerable<ClientDTO>>(clients);
                var productMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
                var products = unitOfWork.Products.GetAll();
                var productDtos = productMapConfig.CreateMapper().Map<IEnumerable<ProductDTO>>(products);

                return (clientDtos, productDtos);
            }
        }
    }
}
