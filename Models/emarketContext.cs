using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace emarket.Models
{
    public class emarketContext : DbContext
    {
       public DbSet<Category> Categories { get; set; }
       public DbSet<Product> Products { get; set; }

       
    }
}