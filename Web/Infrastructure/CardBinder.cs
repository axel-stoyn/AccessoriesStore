using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Organization;
using System.Web.Mvc;

namespace Web.Infrastructure
{
    public class CardBinder : IModelBinder
    {
        private const string sessionKey = "Card";

        public object BindModel(ControllerContext contCont, ModelBindingContext modCont)
        {
            Card card = null;
            if (contCont.HttpContext.Session != null)
            {
                card = (Card)contCont.HttpContext.Session[sessionKey];
            }
            if(card == null)
            {
                card = new Card();
                if(contCont.HttpContext.Session != null)
                {
                    contCont.HttpContext.Session[sessionKey] = card;
                }
            }
            return card;
        }
    }
}