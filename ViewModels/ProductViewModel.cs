using emarket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace emarket.ViewModels
{
    public class ProductViewModel
    {
        public int id { get; set; }
        public string name { get; set; }

        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        public string category_name { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public int category_id { get; set; }
        public List<Category> categories { get; set; }
    }

    
}