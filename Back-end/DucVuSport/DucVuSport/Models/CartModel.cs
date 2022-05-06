using DataAccessLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DucVuSport.Models
{
    public class CartItem
    {
       public Product shopping_product { get; set; }
       public int quantity { get; set; }
    }

    public class Cart
    {
        List<CartItem> _items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return _items; }
        }
        public void Add(Product product, int quantity = 1)
        {
            var item = _items.FirstOrDefault(x=>x.shopping_product.productID == product.productID);
            if (item == null)
            {
                _items.Add(new CartItem
                {
                    shopping_product = product,
                    quantity = quantity
                });
            }
            else
            {
                item.quantity += 1;
            }
        }
    }
}