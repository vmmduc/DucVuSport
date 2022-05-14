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

        public ActionResult AddToCart(int id, int quantity)
        {
            ProductDAO products = new ProductDAO();
            Product product = products.getProductByID(id);
            {
                if (Session["cart"] == null)
                {
                    List<CartModel> cartList = new List<CartModel>();
                    cartList.Add(new CartModel
                    {
                        product = product,
                        quantity = quantity
                    });
                    Session["cart"] = cartList;
                    Session["count"] = cartList.Count;
                }
                else
                {
                    List<CartModel> cartList = Session["cart"] as List<CartModel>;

                    // Kiểm tra sản phẩm đã nằm trong giỏ hàng hay chưa
                    int index = isExist(id);
                    if (index != -1) // Nếu đã có trong giỏ hàng thì tăng số lượng lên 1
                    {
                        cartList[index].quantity += quantity;
                    }
                    else
                    {
                        // Nếu sản phẩm chưa có trong giỏ hàng thì thêm mới
                        cartList.Add(new CartModel
                        {
                            product = product,
                            quantity = quantity
                        });
                    }
                    Session["cart"] = cartList;
                    Session["count"] = cartList.Count;
                }
            }
            
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

       
        public ActionResult Remove(int id)
        {
            List<CartModel> cartList = Session["cart"] as List<CartModel>;
            cartList.RemoveAll(x => x.product.ProductID == id);
            Session["cart"] = cartList;
            Session["count"] = cartList.Count;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        public ActionResult Update(int id, int quantity)
        {
            List<CartModel> cartList = Session["cart"] as List<CartModel>;
           cartList.Find(x => x.product.ProductID == id).quantity = quantity;
            Session["cart"] = cartList;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int id)
        {
            List<CartModel> cart = Session["cart"] as List<CartModel>;
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].product.ProductID == id)
                    return i;
            return -1;
        }
    }
}