using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ClientController : Controller
    {
        ClientService _service;

        public ClientController()
        {
            string info = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _service = new ClientService(info);
        }

        public ActionResult Index()
        {
            IEnumerable<ClientDTO> clientDTOs;
            try
            {
                clientDTOs = _service.GetAll();
            }
            catch
            {
                return View("Error");
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<IEnumerable<ClientViewModel>>(clientDTOs);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            ClientDTO clientDTO;
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
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<ClientViewModel>(clientDTO);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new ClientViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
            var item = mapperConfig.CreateMapper().Map<ClientDTO>(model);
            try
            {
                _service.Add(item);
            }
            catch (ValidationException)
            {

                throw;
            }
            catch
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ClientDTO clientDTO;
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

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<ClientViewModel>(clientDTO);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientViewModel, ClientDTO>());
            var item = mapperConfig.CreateMapper().Map<ClientDTO>(model);
            try
            {
                _service.Edit(id, item);

            }
            catch (ValidationException)
            {

                throw;
            }
            catch
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

/*        public ActionResult Delete(int id)
        {
            var clientDTO = _service.Get(id);
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientViewModel>());
            var model = mapperConfig.CreateMapper().Map<ClientViewModel>(clientDTO);
            return View(model);
        }
*/
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (DALException)
            {

                throw;
            }
            catch
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}
