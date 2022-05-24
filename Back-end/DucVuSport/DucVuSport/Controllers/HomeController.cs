using DataAccessLib.Entities;
using DucVuSport.Models;
using DucVuSport.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ssport.Controllers
{
    public class HomeController : Controller
    {
        dataContext data = new dataContext();
        public const string USER_SESSION = "user";
        public ActionResult Index()
        {
            ViewBag.top10 = data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(string username, string password, bool remember)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Encrypt.GetMD5(password);
                var result = data.Accounts.Where(x => x.Username == username && x.PasswordHash == passwordHash).FirstOrDefault();
                if (result != null)
                {
                    Session[USER_SESSION] = result;
                    if (remember)
                    {
                        Response.Cookies["username"].Value = result.Username;
                        Response.Cookies["username"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["password"].Value = result.PasswordHash;
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var hasEmail = data.Accounts.Count(x => x.Username == model.username) > 0;
                if (hasEmail)
                    return Json(false, JsonRequestBehavior.AllowGet);
                else
                {
                    Account user = new Account();
                    user.Username = model.username;
                    var passwordHash = Encrypt.GetMD5(model.password);
                    user.PasswordHash = passwordHash;

                    data.Accounts.Add(user);
                    data.SaveChanges();

                    var _user = data.Accounts.Where(x => x.Username == model.username && x.PasswordHash == passwordHash).FirstOrDefault();
                    Session[USER_SESSION] = _user;
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (Session[USER_SESSION] != null)
                {
                    var oldPass = Encrypt.GetMD5(model.oldPassword);
                    var user = data.Accounts.Find(((Account)Session["user"]).AccountID);
                    if (user.PasswordHash == oldPass)
                    {
                        var newPass = Encrypt.GetMD5(model.newPassword);
                        user.PasswordHash = newPass;
                        data.SaveChanges();
                        Session[USER_SESSION] = user;
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            DateTime now = DateTime.Now; // Lấy thời gian hiện tại
            var _user = data.Accounts.Find(((Account)Session["user"]).AccountID);
            _user.LastActivity = now;
            data.SaveChanges();

            Session.Remove(USER_SESSION);
            return RedirectToAction("Index");
        }
    }
}