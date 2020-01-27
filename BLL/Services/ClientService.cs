using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL;
using DAL.DataModels;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ClientService : IGeneralService<ClientDTO>
    {
        private string _connectionInfo;

        public ClientService(string connectionInfo)
        {
            _connectionInfo = connectionInfo;
        }

        public IEnumerable<ClientDTO> GetAll()
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>());
                var items = unitOfWork.Clients.GetAll();
                IEnumerable<ClientDTO> dtos;
                try
                {
                    dtos = mapConfig.CreateMapper().Map<IEnumerable<ClientDTO>>(items);

                }
                catch
                {
                    throw new DatabaseException();
                }
                return dtos;
            }
        }

        public ClientDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>());
                var item = unitOfWork.Clients.Get(id);
                ClientDTO itemDto;
                try
                {
                    itemDto = mapConfig.CreateMapper().Map<ClientDTO>(item);
                }
                catch
                {
                    throw new DatabaseException();
                }
                return itemDto;
            }
        }

        public void Add(ClientDTO itemDTO)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>());
                var item = mapConfig.CreateMapper().Map<Client>(itemDTO);
                unitOfWork.Clients.Add(item);
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

        public void Edit(int id, ClientDTO item)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>());
                var entity = unitOfWork.Clients.Get(id);
                unitOfWork.Clients.Update(mapConfig.CreateMapper().Map(item, entity));
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
                var item = unitOfWork.Clients.Get(id);
                unitOfWork.Clients.Delete(item);
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
