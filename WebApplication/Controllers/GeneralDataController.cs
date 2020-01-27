﻿using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    [Authorize]
    public abstract class GeneralDataController<TService, TModelDTO, TViewModel> : Controller
        where TService : IGeneralService<TModelDTO>
        where TModelDTO : class, new()
        where TViewModel : class, new()
    {
        protected TService _service;

        public virtual ActionResult Index()
        {
            IEnumerable<TModelDTO> clientDTOs;
            try
            {
                clientDTOs = _service.GetAll();
            }
            catch
            {
                return View("Error");
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TModelDTO, TViewModel>());
            var model = mapperConfig.CreateMapper().Map<IEnumerable<TViewModel>>(clientDTOs);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public virtual ActionResult Create()
        {
            var model = new TViewModel();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public virtual ActionResult Create(TViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TViewModel, TModelDTO>());
            var item = mapperConfig.CreateMapper().Map<TModelDTO>(model);
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

        [Authorize(Roles = "Admin")]
        public virtual ActionResult Edit(int id)
        {
            TModelDTO clientDTO;
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

            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TModelDTO, TViewModel>());
            var model = mapperConfig.CreateMapper().Map<TViewModel>(clientDTO);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public virtual ActionResult Edit(int id, TViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<TViewModel, TModelDTO>());
            var item = mapperConfig.CreateMapper().Map<TModelDTO>(model);
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public virtual ActionResult Delete(int id)
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
            return null;
        }
    }
}