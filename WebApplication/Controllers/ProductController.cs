using BLL.DTO;
using BLL.Services;
using System.Configuration;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ProductController : GeneralDataController<ProductService, ProductDTO, ProductViewModel>
    {
        public ProductController()
        {
            string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _service = new ProductService(info);
        }
    }
}
