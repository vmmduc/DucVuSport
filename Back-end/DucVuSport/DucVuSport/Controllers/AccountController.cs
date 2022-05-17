using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataAccessLib.Entities;
using DucVuSport.Models;

namespace DucVuSport.Controllers
{
    public class AccountController : Controller
    {
        public const string USER_SESSION = "user";
        dataContext data = new dataContext();
        // GET: Accont
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = data.Accounts.Where(x => x.Email == model.email && x.PasswordHash == model.passwd).FirstOrDefault();
                if (result != null)
                {
                    Session[USER_SESSION] = result;
                    if (model.remember)
                    {
                        /*
                         * Lưu cookie
                         */
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                }
            }
            else
            {
                ModelState.AddModelError("", "Bạn chưa nhập email hoặc mật khẩu");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var hasEmail = data.Accounts.Count(x => x.Email == model.email) > 0;
                if (hasEmail)
                    ModelState.AddModelError("", "Email đã tồn tại");
                else
                {
                    Account user = new Account();
                    user.FullName = model.fullname;
                    user.Email = model.email;
                    user.PhoneNumber = model.phonenumber;
                    user.PasswordHash = model.pwd;

                    data.Accounts.Add(user);
                    data.SaveChanges();

                    var _user = data.Accounts.Where(x => x.Email == model.email && x.PasswordHash == model.pwd).FirstOrDefault();
                    Session[USER_SESSION] = _user;
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LogOut()
        {
            DateTime now = DateTime.Now; // Lấy thời gian hiện tại

            dataContext data = new dataContext();
            var _user = data.Accounts.Find(((Account)Session["user"]).AccountID);
            _user.LastActivity = now;
            data.SaveChanges();

            Session.Remove(USER_SESSION);
            return RedirectToAction("Index", "Home");
        }
    }
}