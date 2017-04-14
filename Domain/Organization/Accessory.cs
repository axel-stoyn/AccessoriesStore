using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Organization
{
    public class Accessory
    {
        public int AccessoryId { get; set; }
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Enter positive values")]
        public decimal Cost { get; set; }
        public string Description { get; set; }

        [Display(Name = "Category")]
        public string Cathegory { get; set; }
        //public byte[] ImageData { get; set; }
    }
}