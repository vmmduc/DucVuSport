using DucVuSport.Models;
using DucVuSport.Models.Entities;
using DucVuSport.Utilities;
using Facebook;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DucVuSport.Controllers
{
    public class UserController : Controller
    {
        private readonly dataContext _data = new dataContext();
        public const string USER_SESSION = "user";

        public ActionResult ShowModal()
        {
            return PartialView("__Account-modal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Encrypt.GetMD5(login.password);
                var result = _data.Accounts.Where(x => x.Email == login.email && x.PasswordHash == passwordHash).FirstOrDefault();
                if (result != null)
                {
                    Session[USER_SESSION] = result;
                    if (login.remember)
                    {
                        Response.Cookies["email"].Value = result.Email;
                        Response.Cookies["email"].Expires = DateTime.Now.AddDays(30);
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
                return null;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var hasEmail = _data.Accounts.Count(x => x.Email == model.email) > 0;
                if (hasEmail)
                    return Json(false, JsonRequestBehavior.AllowGet);
                else
                {
                    Account user = new Account();
                    user.Email = model.email;
                    var passwordHash = Encrypt.GetMD5(model.password);
                    user.PasswordHash = passwordHash;

                    _data.Accounts.Add(user);
                    _data.SaveChanges();

                    var _user = _data.Accounts.Where(x => x.Email == model.email && x.PasswordHash == passwordHash).FirstOrDefault();
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
                    var user = _data.Accounts.Find(((Account)Session["user"]).AccountID);
                    if (user.PasswordHash == oldPass)
                    {
                        var newPass = Encrypt.GetMD5(model.newPassword);
                        user.PasswordHash = newPass;
                        _data.SaveChanges();
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

        [Route("Logout")]
        public ActionResult Logout()
        {
            DateTime now = DateTime.Now;
            var _user = _data.Accounts.Find(((Account)Session["user"]).AccountID);
            _user.LastActivity = now;
            _data.SaveChanges();
            Session.Remove(USER_SESSION);
            return Redirect("/Home/Index");
        }

        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(_data.Accounts, "AccountID", "Email");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            var user = Session["user"] as DucVuSport.Models.Entities.Account;
            if (ModelState.IsValid)
            {
                customer.AccountID = user.AccountID;
                _data.Customers.Add(customer);
                _data.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(customer);
        }


        // Edit infor
        [Route("Edit")]
        public ActionResult Edit()
        {
            Account user = Session["user"] as DucVuSport.Models.Entities.Account;
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _data.Customers.FirstOrDefault(x => x.AccountID == user.AccountID);
            if (customer == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            var user = Session["user"] as DucVuSport.Models.Entities.Account;
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                customer = _data.Customers.FirstOrDefault(x=>x.AccountID == user.AccountID);
                _data.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }
    }
}