using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySpace.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int TotalQuantity { get; internal set; }
    }
}