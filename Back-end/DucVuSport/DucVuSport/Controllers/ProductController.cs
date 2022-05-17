﻿using System;
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
        dataContext data = new dataContext();
        [Route("Product")]
        public ActionResult Index()
        {
            ViewBag.Category_list = data.Categories.ToList();
            return View();
        }
        public ActionResult getProduct()
        {
            List<Product> li = data.Products.ToList();
            return PartialView("__Product", li);
        }

        public ActionResult getProductByCart(int id)
        {
            List<Product> li = data.Products.Where(x => x.CategoryID == id).ToList();
            return PartialView("__Product", li);
        }

        public ActionResult Detail(int id)
        {
            ViewBag.detail = data.Products.Where(x => x.ProductID == id).FirstOrDefault();
            return View();
        }
    }
}