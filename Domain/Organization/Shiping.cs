using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Organization
{
    public class Shiping
    {
        [Display(Name = "Order")]
        public string ShippingId { get; set; }

        [Required(ErrorMessage = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adress")]
        public string Adress { get; set; }

        public DateTime Date { get; set; }
    }
}