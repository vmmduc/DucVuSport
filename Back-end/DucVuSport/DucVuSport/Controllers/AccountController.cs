using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataAccessLib.Entities;
using DucVuSport.Models;
using DucVuSport.Utilities;

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
                var passwordHash = Encrypt.GetMD5(model.password);
                var result = data.Accounts.Where(x => x.Username == model.username && x.PasswordHash == passwordHash).FirstOrDefault();
                if (result != null)
                {
                    Session[USER_SESSION] = result;
                    if (model.remember)
                    {
                        Response.Cookies["username"].Value = result.Username;
                        Response.Cookies["username"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["password"].Value = result.PasswordHash;
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var hasEmail = data.Accounts.Count(x => x.Username == model.username) > 0;
                if (hasEmail)
                    ModelState.AddModelError("", "Username đã tồn tại");
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
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
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
                    }   
                }
                else
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            DateTime now = DateTime.Now; // Lấy thời gian hiện tại
            var _user = data.Accounts.Find(((Account)Session["user"]).AccountID);
            _user.LastActivity = now;
            data.SaveChanges();

            Session.Remove(USER_SESSION);
            return RedirectToAction("Index", "Home");
        }
    }
}