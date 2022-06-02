﻿using DucVuSport.Models;
using DucVuSport.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DucVuSport.Controllers
{
    public class CartController : Controller
    {
        private readonly dataContext _data = new dataContext();

        [Route("cart")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int id, int quantity)
        {
            try
            {
                Product product = _data.Products.Where(x => x.ProductID == id).FirstOrDefault();
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
                        int index = IsExist(id);
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

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
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
        private int IsExist(int id)
        {
            List<CartModel> cart = Session["cart"] as List<CartModel>;
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].product.ProductID == id)
                    return i;
            return -1;
        }

        public ActionResult Payment()
        {
            Customer customer = null;
            var user = Session["user"] as DucVuSport.Models.Entities.Account;
            if (user != null)
            {
                customer = _data.Customers.FirstOrDefault(x=>x.AccountID == user.AccountID);
            }
            return View(customer);
        }
    }
}