using DucVuSport.Models.Entities;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace DucVuSport.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    public class HomeAdminController : Controller
    {
        private readonly DataContext data = new DataContext();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            ViewBag.viewTotal = data.Products.Sum(x => x.ViewCount);
            ViewBag.top10 = data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            var order = data.Orders.Include(o => o.OrderStatus).Include(o => o.Payment).Include(o => o.User).ToList();
            return View(order);
        }
    }
}