using DucVuSport.Common;
using DucVuSport.Models;
using DucVuSport.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DucVuSport.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly DataContext _data = new DataContext();

        [Route("cart")]
        public ActionResult Index()
        {
            var user = Session[Common.Constans.Session.LOGIN_SESSION];
            return View(user);
        }
        public ActionResult GetCartHeader()
        {
            var cartList = Session[Common.Constans.Session.CART_SESSION] as List<DucVuSport.Models.CartModel>;
            return PartialView("__header-cart", cartList);
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            try
            {
                Product product = _data.Products.Where(x => x.ProductID == id).FirstOrDefault();
                {
                    if (Session[Common.Constans.Session.CART_SESSION] == null)
                    {
                        List<CartModel> cartList = new List<CartModel>();
                        cartList.Add(new CartModel
                        {
                            product = product,
                            quantity = quantity
                        });
                        Session[Constans.Session.CART_SESSION] = cartList;
                        Session["count"] = cartList.Count;
                    }
                    else
                    {
                        List<CartModel> cartList = Session[Common.Constans.Session.CART_SESSION] as List<CartModel>;
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
                        Session[Common.Constans.Session.CART_SESSION] = cartList;
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
            List<CartModel> cartList = Session[Common.Constans.Session.CART_SESSION] as List<CartModel>;
            cartList.RemoveAll(x => x.product.ProductID == id);
            Session["cart"] = cartList;
            Session["count"] = cartList.Count;
            return RedirectToAction("GetCartHeader", "Cart");
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
            List<CartModel> cart = Session[Common.Constans.Session.CART_SESSION] as List<CartModel>;
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].product.ProductID == id)
                    return i;
            return -1;
        }


        [HttpPost]
        public ActionResult Order(User model)
        {
            var user = Session[Constans.Session.LOGIN_SESSION] as User;
            if (user != null)
            {
                var customer = _data.Users.Find(user.UserID);
                customer.PhoneNumber = model.PhoneNumber;
                customer.Province = model.Province;
                customer.District = model.District;
                customer.Ward = model.Ward;
                customer.AddressDetail = model.AddressDetail;
                _data.SaveChanges();
                CreateOrder(customer);
            }
            else
            {
                user = model;
                _data.Users.Add(user);
                _data.SaveChanges();
                CreateOrder(user);
            }
            return RedirectToAction("Success");
        }
        private void CreateOrder(User user)
        {
            long? orderTotal = 0;
            var cart = Session[Constans.Session.CART_SESSION] as List<CartModel>;
            // Table order
            var order = new Order();
            order.CustomerID = user.UserID;
            order.OrderDate = DateTime.Now;
            order.Paid = false;
            order.PaymentID = 1;
            order.PaymentDate = DateTime.Now;
            order.Status = 1;
            _data.Orders.Add(order);
            _data.SaveChanges();
            int orderId = order.OrderID;
            foreach (var item in cart) // Duyệt trong cart session
            {
                var product = _data.Products.FirstOrDefault(x => x.ProductID == item.product.ProductID);
                if (product != null)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = orderId;
                    orderDetail.ProductID = product.ProductID;
                    orderDetail.Discount = (float?)product.Discount;
                    orderDetail.Quantity = item.quantity;
                    orderDetail.Total = ((long?)(product.Price - product.Price * product.Discount) * orderDetail.Quantity);
                    _data.OrderDetails.Add(orderDetail);
                    orderTotal += orderDetail.Total;
                    product.Quantity -= item.quantity;
                }
            }
            order.Total = orderTotal;
            //SendEmail(user);
            _data.SaveChanges();
            Session.Remove(Common.Constans.Session.CART_SESSION);
        }
        private void SendEmail(User user)
        {
            try
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Common/MailTemplate/Template01.htm"));
                content = content.Replace("[FullName]", user.FullName);
                content = content.Replace("[PhoneNumber]", user.PhoneNumber);
                content = content.Replace("[Email]", user.Email);
                content = content.Replace("[Address]", user.AddressDetail + user.Ward + user.District + user.Province);

                new SendEmail().Send(user.Email, "Thông báo đặt hàng thành công", content);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}