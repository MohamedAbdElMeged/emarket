using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emarket.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public double product_price { get; set; }
        public string product_image{ get; set; }
        public int category_id { get; set; }
        public string category_name { get; set; }



    }
}