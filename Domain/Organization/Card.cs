using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Organization;

namespace Domain.Organization
{
    public class Card
    {
        private List<CardLine> cardCollection = new List<CardLine>();

        public void Add(Accessory accessory, int quantity)
        {
            CardLine addCard = cardCollection
                .Where(a => a.Accessory.AccessoryId == accessory.AccessoryId)
                .FirstOrDefault();

            if(addCard == null)
            {
                cardCollection.Add(new CardLine
                {
                    Accessory = accessory,
                    Quantity = quantity
                });
            }
            else
            {
                addCard.Quantity += quantity;
            }
        }

        public void Remove(Accessory accessory)
        {
            cardCollection.RemoveAll(a => a.Accessory.AccessoryId == accessory.AccessoryId);
        }

        public decimal TotalCost()
        {
            return cardCollection.Sum(s => s.Accessory.Cost * s.Quantity);
        }

        public void Delite()
        {
            cardCollection.Clear();
        }

        public IEnumerable<CardLine> Cards
        {
            get { return cardCollection; }
        }

        public class CardLine
        {
            public Accessory Accessory { get; set; }
            public int Quantity { get; set; }
        }

    }
}