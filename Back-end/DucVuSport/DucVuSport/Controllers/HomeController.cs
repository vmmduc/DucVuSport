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
        dataContext data = new dataContext();
        public ActionResult Index()
        {
            ViewBag.top10 = data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            return View();
        }
    }

    
}