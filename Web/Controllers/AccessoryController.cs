using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AccessoryController : Controller
    {

        private IAccessory repository;
        public int pageSize = 10;

        public AccessoryController(IAccessory repo)
        {
            repository = repo;
        }

        //List Accessories with pages and sort
        public ViewResult List(string cathegory, int page = 1)
        {
            AccessoriesList list = new AccessoriesList
            {
                Accessories = repository.Accessories
                .Where(a => cathegory == null || a.Cathegory == cathegory)
                .OrderBy(access => access.AccessoryId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPage = pageSize,
                    TotalItems = cathegory == null ? repository.Accessories.Count() : repository.Accessories.Where(a => a.Cathegory == cathegory).Count()
                },
                Category = cathegory
            };
            return View(list);
        }
    }
}