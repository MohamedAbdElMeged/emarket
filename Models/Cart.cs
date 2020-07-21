using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace emarket.Models
{
    public class Cart
    {
        [Key]
        public int Product_Id { get; set; }
        public DateTime Added_At { get; set; }
    }
}