using DataAccessLib.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DucVuSport.Models;
using DataAccessLib.Entities;

namespace DucVuSport.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            ProductDAO product = new ProductDAO();
            ViewBag.Category_list = product.getAllCategory();
            return View();
        }
        public ActionResult getProduct()
        {
            dataContext data = new dataContext();
            List<Product> li = data.Products.ToList();
            return PartialView("__Product", li);
        }

        public ActionResult getProductByCart(string id)
        {
            dataContext data = new dataContext();
            List<Product> li = data.Products.Where(x=>x.categoryID == id).ToList();
            return PartialView("__Product", li);
        }
        public ActionResult Detail(string id)
        {
            ProductDAO products = new ProductDAO();
            ViewBag.detail = products.getProductDetail(id);
            return View();
        }

      
    }
}