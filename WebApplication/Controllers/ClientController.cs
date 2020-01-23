using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ClientController : GeneralDataController<ClientService, ClientDTO, ClientViewModel>
    {
        public ClientController()
        {
            string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _service = new ClientService(info);
        }
    }
}
