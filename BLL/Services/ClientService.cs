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
                return mapConfig.CreateMapper().Map<IEnumerable<ClientDTO>>(unitOfWork.Clients.GetAll());
            }
        }
    }
}
