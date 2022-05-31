using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DucVuSport.Models;
using DucVuSport.Models.Entities;

namespace DucVuSport.Controllers
{
    public class ProductController : Controller
    {
        private readonly dataContext _data = new dataContext();


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

        public ActionResult Feedback()
        {
            return PartialView("__Form-vote");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Feedback(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                var result = _data.Products.FirstOrDefault(x => x.ProductID == feedBack.ProductID);
                if (result != null)
                {
                    try
                    {
                        _data.FeedBacks.Add(feedBack);
                        _data.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}