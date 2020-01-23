using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ProductService : IGeneralService<ProductDTO>
    {
        string _connectionInfo;

        public ProductService(string connectionInfo)
        {
            _connectionInfo = connectionInfo;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
                var items = unitOfWork.Products.GetAll();
                return mapConfig.CreateMapper().Map<IEnumerable<ProductDTO>>(items);
            }
        }

        public ProductDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
                var item = unitOfWork.Products.Get(id);
                return mapConfig.CreateMapper().Map<ProductDTO>(item);
            }
        }

        public void Add(ProductDTO itemDTO)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
                var item = mapConfig.CreateMapper().Map<Product>(itemDTO);
                unitOfWork.Products.Add(item);
                unitOfWork.SaveChanges();
            }
        }

        public void Edit(int id, ProductDTO item)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
                var entity = unitOfWork.Products.Get(id);
                unitOfWork.Products.Update(mapConfig.CreateMapper().Map(item, entity));
                unitOfWork.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var item = unitOfWork.Products.Get(id);
                unitOfWork.Products.Delete(item);
                unitOfWork.SaveChanges();
            }
        }
    }
}
