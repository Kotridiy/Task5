using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ProductService : IGeneralService<ProductDTO>
    {
        public string _connectionInfo;

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
                IEnumerable<ProductDTO> dtos;
                try
                {
                    dtos = mapConfig.CreateMapper().Map<IEnumerable<ProductDTO>>(items);

                }
                catch
                {
                    throw new DatabaseException();
                }
                return dtos;
            }
        }

        public ProductDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
                var item = unitOfWork.Products.Get(id);
                ProductDTO itemDto;
                try
                {
                    itemDto = mapConfig.CreateMapper().Map<ProductDTO>(item);
                }
                catch
                {
                    throw new DatabaseException();
                }
                return itemDto;
            }
        }

        public void Add(ProductDTO itemDTO)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
                var item = mapConfig.CreateMapper().Map<Product>(itemDTO);
                unitOfWork.Products.Add(item);
                try
                {
                    unitOfWork.SaveChanges();
                }
                catch
                {
                    throw new DatabaseException();
                }
            }
        }

        public void Edit(int id, ProductDTO item)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, Product>());
                var entity = unitOfWork.Products.Get(id);
                unitOfWork.Products.Update(mapConfig.CreateMapper().Map(item, entity));
                try
                {
                    unitOfWork.SaveChanges();
                }
                catch
                {
                    throw new DatabaseException();
                }
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var item = unitOfWork.Products.Get(id);
                unitOfWork.Products.Delete(item);
                try
                {
                    unitOfWork.SaveChanges();
                }
                catch
                {
                    throw new DatabaseException();
                }
            }
        }
    }
}
