using Domain.Organization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Domain.Context
{
    public class AccessoriesDbContext : DbContext
    {
        public DbSet<Accessory> Accessories { get; set; }
    }
}