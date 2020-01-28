using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class SoldProductController : Controller
    {
        private SoldProductService _service;
        private SoldProductService Service
        {
            get
            {
                if (_service == null)
                {
                    string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    _service = new SoldProductService(info);
                }
                return _service;
            }
            set => _service = value;
        }

        public SoldProductController()
        {
            string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Service = new SoldProductService(info);
        }

        public ActionResult Index()
        {
            IEnumerable<SoldProductDTO> productDTOs;
            try
            {
                productDTOs = Service.GetAll();
            }
            catch (DatabaseException)
            {
                return View("Error");
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductDTO, DetailSoldProductViewModel>());
            var model = mapperConfig.CreateMapper().Map<IEnumerable<DetailSoldProductViewModel>>(productDTOs);
            return View(model);
        }

        public ActionResult MakeOrder()
        {
            (var clients, var products) = Service.GetData();
            SelectList clientList = new SelectList(clients, nameof(ClientDTO.Id), nameof(ClientDTO.Name));
            SelectList productList = new SelectList(products, nameof(ProductDTO.Id), nameof(ProductDTO.Name));
            ViewBag.Clients = clientList;
            ViewBag.Products = productList;
            var model = new CreateSoldProductViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MakeOrder(CreateSoldProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CreateSoldProductViewModel, SoldProductDTO>());
            var item = mapperConfig.CreateMapper().Map<SoldProductDTO>(model);
                Service.Add(item, model.ClientId, model.ProductId);
            try
            {
            }
            catch (ValidationException)
            {

                throw;
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ProductSearch()
        {
            IEnumerable<SoldProductDTO> productDTOs;
            try
            {
                productDTOs = Service.Search(Request.Form["price"], Request.Form["manager"], Request.Form["client"], Request.Form["or_and"]);
            }
            catch (DatabaseException)
            {
                return View("Error");
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductDTO, DetailSoldProductViewModel>());
            var model = mapperConfig.CreateMapper().Map<IEnumerable<DetailSoldProductViewModel>>(productDTOs);
            return PartialView(model);
        }
    }
}
