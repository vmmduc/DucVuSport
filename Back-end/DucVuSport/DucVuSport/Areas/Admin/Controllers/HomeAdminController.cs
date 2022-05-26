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
        dataContext data = new dataContext();
        public ActionResult Index()
        {
            ViewBag.viewTotal = data.Products.Sum(x => x.View_count);
            ViewBag.top10 = data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            return View();
        }
    }
}