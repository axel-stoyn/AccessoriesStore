using Domain.Abstract;
using Domain.Context;
using Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CardController : Controller
    {
        private AccessoriesDbContext db = new AccessoriesDbContext();
        private IAccessory repository;
        private IOrder order;
        public CardController(IAccessory repo, IOrder ord)
        {
            repository = repo;
            order = ord;
        }

        public ViewResult Index(Card card, string returnUrl)
        {
            return View(new CardToBye
            {
                Card = card,
                ReturnUrl = returnUrl
            });
        }

        //Add Items to card (basket)
        public RedirectToRouteResult AddToCard(Card card, int accessoryId, string returnUrl)
        {
            Accessory accessory = repository.Accessories.FirstOrDefault(g => g.AccessoryId == accessoryId);

            if (accessory != null)
            {
                card.Add(accessory, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //Delete Items to card (basket)
        public RedirectToRouteResult RemoveFromCart(Card card, int accessoryId, string returnUrl)
        {
            Accessory accessory = repository.Accessories.FirstOrDefault(g => g.AccessoryId == accessoryId);

            if (accessory != null)
            {
                card.Remove(accessory);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Card card)
        {
            return PartialView(card);
        }

        //Order Items with card (basket)
        public ViewResult Order()
        {
            return View(new Shiping());
        }

        [HttpPost]
        public ViewResult Order(Card card, Shiping shiping)
        {
            if(card.Cards.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your card is empty");
            }
            if(ModelState.IsValid)
            {
                order.ProcessOrder(card, shiping);
                card.Delite();
                return View("Completed");
            }
            else
            {
                return View(shiping);
            }
        }

        //Search concrete item. Don't work
        [HttpPost]
        public ActionResult Search(string name)
        {
            var accessories = db.Accessories.Where(t => t.Name.Contains(name)).ToList();
            if (accessories.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(accessories);
        }
    }
}
