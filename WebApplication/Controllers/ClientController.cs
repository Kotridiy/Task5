using BLL.DTO;
using BLL.Services;
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
