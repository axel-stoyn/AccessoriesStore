using Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Abstract
{
    public interface IOrder
    {
        void ProcessOrder(Card card, Shiping shiping);
    }
}