using Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class AccessoriesList
    {
        public IEnumerable<Accessory> Accessories { get; set; }
        public PageInfo PageInfo { get; set; }
        public string Category { get; set; }
    }
}