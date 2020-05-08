using emarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emarket.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

    }
}