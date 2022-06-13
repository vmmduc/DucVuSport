using DucVuSport.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ssport.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _data = new DataContext();

        public ActionResult Index()
        {
            ViewBag.TopProduct = _data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            ViewBag.Equipment = _data.Products.Where(x => x.CategoryID == 2).Take(10).ToList();
            return View();
        }
    }
}