using BLL.DTO;
using BLL.Services;
using System.Configuration;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ClientController : GeneralDataController<ClientService, ClientDTO, ClientViewModel>
    {
        private ClientService _service;
        protected override ClientService Service 
        {
            get
            {
                if (_service == null)
                {
                    string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    _service = new ClientService(info);
                }
                return _service;
            }
            set => _service = value; 
        }

        [Authorize(Roles = "User")]
        public override ActionResult Create()
        {
            return base.Create();
        }

        [Authorize(Roles = "User")]
        public override ActionResult Create(ClientViewModel model)
        {
            return base.Create(model);
        }
    }
}
