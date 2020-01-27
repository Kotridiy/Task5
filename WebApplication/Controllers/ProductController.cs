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
        private ProductService _service;
        protected override ProductService Service
        {
            get
            {
                if (_service == null)
                {
                    string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    _service = new ProductService(info);
                }
                return _service;
            }
            set => _service = value;
        }

        [AllowAnonymous]
        public JsonResult JsonProductCount()
        {
            var productCounts = Service.GetAll().Select(item => new ArrayList() { item.Name, item.Count });
            return Json(productCounts);
        }
    }
}
