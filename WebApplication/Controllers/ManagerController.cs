using BLL.DTO;
using BLL.Services;
using System.Configuration;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ManagerController : GeneralDataController<ManagerService, ManagerDTO, ManagerViewModel>
    {
        public ManagerController()
        {
            string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _service = new ManagerService(info);
        }
    }
}
