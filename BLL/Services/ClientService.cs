using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ClientService
    {
        string _connectionInfo;

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
                return mapConfig.CreateMapper().Map<IEnumerable<ClientDTO>>(items);
            }
        }

        public ClientDTO Get(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>());
                var item = unitOfWork.Clients.Get(id);
                return mapConfig.CreateMapper().Map<ClientDTO>(item);
            }
        }

        public void Add(ClientDTO itemDTO)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>());
                var item = mapConfig.CreateMapper().Map<Client>(itemDTO);
                unitOfWork.Clients.Add(item);
            }
        }

        public void Edit(int id, ClientDTO item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (var unitOfWork = DataAccessBuilder.CreateUnitOfWork(_connectionInfo))
            {
                var item = unitOfWork.Clients.Get(id);
                unitOfWork.Clients.Delete(item);
            }
        }
    }
}
