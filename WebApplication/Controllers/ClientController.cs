using AutoMapper;
using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ClientController : Controller
    {
        ClientService _service;

        public ClientController()
        {
            string info = @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Web-data.mdf;Initial Catalog=aspnet-WebApplication-20200113062835;Integrated Security=True";
            _service = new ClientService(info);
        }

        // GET: Client
        public ActionResult Index()
        {
            var clientDTOs = _service.GetAll();
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<IEnumerable<ClientViewModel>>(clientDTOs);
            return View(model);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            var clientDTO = _service.Get(id);
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<ClientViewModel>(clientDTO);
            return View(model);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            var model = new ClientViewModel();
            return View(model);
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
                var item = mapperConfig.CreateMapper().Map<ClientDTO>(collection);
                _service.Add(item);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            var clientDTO = _service.Get(id);
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<ClientViewModel>(clientDTO);
            return View(model);
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
                var item = mapperConfig.CreateMapper().Map<ClientDTO>(collection);
                _service.Edit(id, item);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            var clientDTO = _service.Get(id);
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<ClientViewModel>(clientDTO);
            return View(model);
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _service.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
