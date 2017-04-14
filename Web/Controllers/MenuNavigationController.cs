using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MenuNavigationController : Controller
    {
        private IAccessory repository;

        public MenuNavigationController(IAccessory repo)
        {
            repository = repo;
        }

        // Menu Navigation for Main List
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> cat = repository.Accessories
                .Select(a => a.Cathegory)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(cat);
        }
    }
}