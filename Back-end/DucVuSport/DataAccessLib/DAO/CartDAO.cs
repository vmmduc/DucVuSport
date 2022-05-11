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
            var hasProduct = data.Carts.FirstOrDefault(x =>x.userID == userID && x.productID == productID);
            if (hasProduct != null)
            {
                hasProduct.quantity += quantity;
            }
            else
            {
                Cart cart = new Cart();
                cart.userID = userID;
                cart.productID = productID;
                cart.quantity = quantity;

                data.Carts.Add(cart);
            }
            data.SaveChanges();
            // Chưa xong
        }
        public Cart GetCartByUser(int id)
        {
            return data.Carts.FirstOrDefault(x=>x.userID == id);
        }
    }
}
