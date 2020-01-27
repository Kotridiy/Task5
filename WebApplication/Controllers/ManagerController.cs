using BLL.DTO;
using BLL.Services;
using System.Configuration;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ManagerController : GeneralDataController<ManagerService, ManagerDTO, ManagerViewModel>
    {
        private ManagerService _service;
        protected override ManagerService Service
        {
            get
            {
                if (_service == null)
                {
                    string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    _service = new ManagerService(info);
                }
                return _service;
            }
            set => _service = value;
        }
    }
}
