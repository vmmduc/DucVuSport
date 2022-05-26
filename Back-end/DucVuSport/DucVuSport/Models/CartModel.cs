using DucVuSport.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class CartModel
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }

}