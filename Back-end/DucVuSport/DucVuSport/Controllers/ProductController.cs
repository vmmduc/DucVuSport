using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DucVuSport.Models.Entities;

namespace DucVuSport.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly DataContext _data = new DataContext();


        [Route("Product")]
        public ActionResult Index()
        {
            ViewBag.Category_list = _data.Categories.ToList();
            return View();
        }


        public ActionResult GetProduct()
        {
            List<Product> li = _data.Products.ToList();
            return PartialView("__Product", li);
        }


        public ActionResult GetProductByCart(int id)
        {
            List<Product> li = _data.Products.Where(x => x.CategoryID == id).ToList();
            return PartialView("__Product", li);
        }


        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                Product product = _data.Products.Where(x => x.ProductID == id).FirstOrDefault();
                product.ViewCount += 1;
                _data.SaveChanges();
                ViewBag.detail = product;
            }
            return View();
        }

        public ActionResult SearchProduct(string keyword)
        { 
            var product_list = _data.Products.Where(x => x.ProductName.Trim().ToLower().Contains(keyword.Trim().ToLower())).ToList();
            return PartialView("__header-search-result", product_list);
        }
    }
}