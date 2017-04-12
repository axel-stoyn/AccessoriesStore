using Domain.Abstract;
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

        public RedirectToRouteResult AddToCard(Card card, int accessoryId, string returnUrl)
        {
            Accessory accessory = repository.Accessories.FirstOrDefault(g => g.AccessoryId == accessoryId);

            if (accessory != null)
            {
                card.Add(accessory, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

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
        //public Card GetCart()
        //{
        //    Card card = (Card)Session["ByeCard"];
        //    if (card == null)
        //    {
        //        card = new Card();
        //        Session["ByeCard"] = card;
        //    }
        //    return card;
        //}
    }
}
