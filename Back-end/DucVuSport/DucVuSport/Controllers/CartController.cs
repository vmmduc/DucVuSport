using DataAccessLib.DAO;
using DataAccessLib.Entities;
using DucVuSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DucVuSport.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(string id, int quantity)
        {
            ProductDAO products = new ProductDAO();
            Product product = products.getProductByID(id);
            if (Session["Cart"] == null)
            {
                List<CartModel> cartList = new List<CartModel>();
                cartList.Add(new CartModel
                {
                    product = product,
                    quantity = quantity
                });
                Session["Cart"] = cartList;
                Session["Count"] = cartList.Count;
            }
            else
            {
                List<CartModel> cartList = Session["cart"] as List<CartModel>;

                // check cart has product yet
                int index = isExist(id);
                if(index != -1)
                {
                    cartList[index].quantity += quantity;
                }
                else
                {
                    // if cart has not product, add product into cart
                    cartList.Add(new CartModel
                    {
                        product = product,
                        quantity= quantity
                    });
                }
                Session["Cart"] = cartList;
                Session["Count"] = cartList.Count;
            }

            return Json( new { Message = "Thành công", JsonRequestBehavior.AllowGet});
        }

        private int isExist(string id)
        {
            List<CartModel> cart = Session["cart"] as List<CartModel>;
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].product.productID == id)
                    return i;
            return -1;
        }

        public ActionResult Remove(string id)
        {
            List<CartModel> cartList = Session["cart"] as List<CartModel>;
            cartList.RemoveAll(x=>x.product.productID == id);
            Session["Cart"] = cartList;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}