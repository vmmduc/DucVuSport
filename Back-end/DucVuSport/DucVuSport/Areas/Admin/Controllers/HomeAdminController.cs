using DucVuSport.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DucVuSport.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    public class HomeAdminController : Controller
    {
        private readonly dataContext data = new dataContext();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            ViewBag.viewTotal = data.Products.Sum(x=>x.ViewCount);
            ViewBag.top10 = data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            return View();
        }
    }
}