using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Organization
{
    public class Accessory
    {
        public int AccessoryId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string Cathegory { get; set; }
        //public byte[] ImageData { get; set; }
    }
}