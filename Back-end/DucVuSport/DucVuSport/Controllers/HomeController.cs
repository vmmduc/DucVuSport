﻿using DataAccessLib.DAO;
using DataAccessLib.Entities;
using DucVuSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssport.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductDAO product = new ProductDAO();
            ViewBag.top10 = product.getTop10();
            return View();
        }
    }

    
}