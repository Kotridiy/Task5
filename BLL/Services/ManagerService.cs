using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ManagerService : IGeneralService<ManagerDTO>
    {
        public string _connectionInfo;

        public ManagerService(string connectionInfo)
        {
            _connectionInfo = connectionInfo;
        }

        public IEnumerable<ManagerDTO> GetAll()
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerDTO>());
                var items = unitOfWork.Managers.GetAll();
                IEnumerable<ManagerDTO> dtos;
                try
                {
                    dtos = mapConfig.CreateMapper().Map<IEnumerable<ManagerDTO>>(items);

                }
                catch
                {
                    throw new DatabaseException();
                }
                return dtos;
            }
        }

        public ManagerDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerDTO>());
                var item = unitOfWork.Managers.Get(id);
                ManagerDTO itemDto;
                try
                {
                    itemDto = mapConfig.CreateMapper().Map<ManagerDTO>(item);
                }
                catch
                {
                    throw new DatabaseException();
                }
                return itemDto;
            }
        }

        public void Add(ManagerDTO itemDTO)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ManagerDTO, Manager>());
                var item = mapConfig.CreateMapper().Map<Manager>(itemDTO);
                unitOfWork.Managers.Add(item);
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

        public void Edit(int id, ManagerDTO item)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ManagerDTO, Manager>());
                var entity = unitOfWork.Managers.Get(id);
                unitOfWork.Managers.Update(mapConfig.CreateMapper().Map(item, entity));
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
                var item = unitOfWork.Managers.Get(id);
                unitOfWork.Managers.Delete(item);
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
