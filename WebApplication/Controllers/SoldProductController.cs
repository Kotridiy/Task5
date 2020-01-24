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
    public class SoldProductController : Controller
    {
        private SoldProductService _service;

        public SoldProductController()
        {
            string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _service = new SoldProductService(info);
        }

        public ActionResult Index()
        {
            IEnumerable<SoldProductDTO> clientDTOs;
            try
            {
                clientDTOs = _service.GetAll();
            }
            catch (DALException)
            {
                return View("Error");
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductDTO, SoldProductViewModel>());
            var model = mapperConfig.CreateMapper().Map<IEnumerable<SoldProductViewModel>>(clientDTOs);
            return View(model);
        }


        public ActionResult Details(int id)
        {
            SoldProductDTO clientDTO;
            try
            {
                clientDTO = _service.Get(id);
            }
            catch (DALException)
            {

                throw;
            }
            catch
            {
                return View("Error");
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductDTO, SoldProductViewModel>());
            var model = mapperConfig.CreateMapper().Map<SoldProductViewModel>(clientDTO);
            return View(model);
        }

        public ActionResult MakeOrder()
        {
            (var clients, var products) = _service.GetData();
            SelectList clientList = new SelectList(clients, nameof(ClientDTO.Id), nameof(ClientDTO.Name));
            SelectList productList = new SelectList(products, nameof(ProductDTO.Id), nameof(ProductDTO.Name));
            ViewBag.Clients = clientList;
            ViewBag.Products = productList;
            var model = new SoldProductViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult MakeOrder(SoldProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<SoldProductViewModel, SoldProductDTO>());
            var item = mapperConfig.CreateMapper().Map<SoldProductDTO>(model);
            try
            {
                _service.Add(item, model.ClientId, model.ProductId);
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
    }
}
