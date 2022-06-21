using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DucVuSport.Models.Entities;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class BlogAdminController : BaseController
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.BlogCategory).Include(b => b.User);
            return View(blogs.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.BlogCatID = new SelectList(db.BlogCategories, "BlogCatID", "CatName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                var user = Session[Common.Constans.Session.ADMIN_SESSION] as User;
                if (user != null)
                {
                    blog.CreateBy = user.UserID;
                    blog.CreateDate = DateTime.Now;
                }
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogCatID = new SelectList(db.BlogCategories, "BlogCatID", "CatName", blog.BlogCatID);
            return View(blog);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogCatID = new SelectList(db.BlogCategories, "BlogCatID", "CatName", blog.BlogCatID);
            ViewBag.CreateBy = new SelectList(db.Users, "UserID", "FullName", blog.CreateBy);
            return View(blog);
        }


        [HttpPost]
        public ActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogCatID = new SelectList(db.BlogCategories, "BlogCatID", "CatName", blog.BlogCatID);
            return View(blog);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
