using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DucVuSport.Models.Entities;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class ProductAdminController : BaseController
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase imageFile)
        {
            DateTime now = DateTime.Now;
            product.CreateDate = now;
            product.Sold = 0; ;
            product.ViewCount = 0;
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string extentsion = Path.GetExtension(imageFile.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName) + extentsion;
                    product.Image = fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Products"), fileName);
                    imageFile.SaveAs(fileName);
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string extentsion = Path.GetExtension(image.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName) + extentsion;
                    product.Image = fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Products"), fileName);
                    image.SaveAs(fileName);
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
