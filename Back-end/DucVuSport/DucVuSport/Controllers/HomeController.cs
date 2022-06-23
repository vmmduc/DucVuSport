using DucVuSport.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ssport.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly DataContext _data = new DataContext();

        public ActionResult Index()
        {
            ViewBag.TopProduct = _data.Products.Where(x => x.Sold > 0).OrderByDescending(x => x.Sold).Take(10).ToList();
            ViewBag.Equipment = _data.Products.Where(x => x.CategoryID == 2).Take(10).ToList();
            return View();
        }
    }
}