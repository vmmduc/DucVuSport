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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product()
        {
            return PartialView("__Product-list");
        }
    }
}