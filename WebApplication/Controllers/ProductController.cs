using BLL.DTO;
using BLL.Services;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
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

        [AllowAnonymous]
        public JsonResult JsonProductCount()
        {
            var productCounts = _service.GetAll().Select(item => new ArrayList() { item.Name, item.Count });
            return Json(productCounts);
        }
    }
}
