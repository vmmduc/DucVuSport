using DataAccessLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.DAO
{
    public class CartDAO
    {
        dataContext data = null;
        CartDAO()
        {
            data = new dataContext();
        }
        public void AddToCart(int userID, int productID, int quantity)
        {
            // Chưa xong
            var hasProduct = data.Carts.FirstOrDefault(x =>x.UserID == userID && x.ProductID == productID);
            if (hasProduct != null)
            {
                hasProduct.Quantity += quantity;
            }
            else
            {
                Cart cart = new Cart();
                cart.UserID = userID;
                cart.ProductID = productID;
                cart.Quantity = quantity;

                data.Carts.Add(cart);
            }
            data.SaveChanges();
            // Chưa xong
        }
        public Cart GetCartByUser(int id)
        {
            return data.Carts.FirstOrDefault(x=>x.UserID == id);
        }
    }
}
