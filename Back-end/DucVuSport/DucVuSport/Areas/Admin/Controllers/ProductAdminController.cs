using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLib.Entities;
using System.IO;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        private dataContext db = new dataContext();

        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);
            ViewBag.Label = "Danh sách sản phẩm";
            return View(await products.ToListAsync());
        }


        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name");
            ViewBag.Label = "Tạo mới sản phẩm";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,ProductName,ShotDescribe,Describe,Image,CategoryID,SupplierID,Price,Discount,Quantity,Create_date,Sold,View_count,Status")] Product product, HttpPostedFileBase imageFile)
        {
            DateTime now = DateTime.Now;
            product.Create_date = now;
            product.Sold = 0; ;
            product.View_count = 0;
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string extentsion = Path.GetExtension(imageFile.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName) + extentsion;
                    product.Image = fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    imageFile.SaveAs(fileName);
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", product.SupplierID);
            return View(product);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", product.SupplierID);
            ViewBag.Label = "Sửa thông tin sản phẩm";
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,ProductName,ShotDescribe,Describe,Image,CategoryID,SupplierID,Price,Discount,Quantity,Create_date,Sold,View_count,Status")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string extention = Path.GetExtension(image.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName) + extention;
                    string path = Server.MapPath("~/Content/images");
                    if (!System.IO.File.Exists(path + fileName))
                    {
                        product.Image = fileName;
                        fileName = Path.Combine(path + fileName);
                        image.SaveAs(fileName);
                    }
                    else
                    {
                        product.Image = fileName;
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", product.SupplierID);
            return View(product);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Label = "Xóa sản phẩm";
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
