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
        [Route("Product")]
        public ActionResult Index()
        {
            ProductDAO product = new ProductDAO();
            ViewBag.Category_list = product.getAllCategory();
            return View();
        }
        public ActionResult getProduct()
        {
            ProductDAO products = new ProductDAO(); 
            List<Product> li = products.getAllProduct();
            return PartialView("__Product", li);
        }

        public ActionResult getProductByCart(int id)
        {
            ProductDAO products = new ProductDAO();
            List<Product> li = products.getProductByCat(id);
            return PartialView("__Product", li);
        }

        public ActionResult Detail(int id)
        {
            ProductDAO products = new ProductDAO();
            ViewBag.detail = products.getProductByID(id);
            ViewBag.image = products.getImage(id);
            return View();
        }
    }
}