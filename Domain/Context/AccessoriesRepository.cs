using Domain.Abstract;
using Domain.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Context
{
    public class AccessoriesRepository : IAccessory
    {
        AccessoriesDbContext context = new AccessoriesDbContext();

        public IEnumerable<Accessory> Accessories
        {
            get { return context.Accessories; }
        }
    }
}