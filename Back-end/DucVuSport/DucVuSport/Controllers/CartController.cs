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
            if (Session["user"] == null)
            /*
             * Nếu session rỗng (Chưa đăng nhập)
             * Thì lưu giỏ hàng vào session
             */
            {
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
                    Session["Cart"] = cartList;
                    Session["Count"] = cartList.Count;
                }
            }
            else
            {
                var user = Session["user"] as tbUser;

            }

            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }

        private int isExist(int id)
        {
            List<CartModel> cart = Session["cart"] as List<CartModel>;
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].product.productID == id)
                    return i;
            return -1;
        }

        public ActionResult Remove(int id)
        {
            List<CartModel> cartList = Session["cart"] as List<CartModel>;
            cartList.RemoveAll(x => x.product.productID == id);
            Session["Cart"] = cartList;
            Session["Count"] = cartList.Count;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }
}