using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ManagerService : IGeneralService<ManagerDTO>
    {
        string _connectionInfo;

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
                return mapConfig.CreateMapper().Map<IEnumerable<ManagerDTO>>(items);
            }
        }

        public ManagerDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerDTO>());
                var item = unitOfWork.Managers.Get(id);
                return mapConfig.CreateMapper().Map<ManagerDTO>(item);
            }
        }

        public void Add(ManagerDTO itemDTO)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ManagerDTO, Manager>());
                var item = mapConfig.CreateMapper().Map<Manager>(itemDTO);
                unitOfWork.Managers.Add(item);
                unitOfWork.SaveChanges();
            }
        }

        public void Edit(int id, ManagerDTO item)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ManagerDTO, Manager>());
                var entity = unitOfWork.Managers.Get(id);
                unitOfWork.Managers.Update(mapConfig.CreateMapper().Map(item, entity));
                unitOfWork.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var item = unitOfWork.Managers.Get(id);
                unitOfWork.Managers.Delete(item);
                unitOfWork.SaveChanges();
            }
        }
    }
}
