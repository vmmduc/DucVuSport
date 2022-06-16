using DucVuSport.Models;
using DucVuSport.Models.Entities;
using DucVuSport.Utilities;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DucVuSport.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _data = new DataContext();

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
                var result = _data.Users.FirstOrDefault(x => x.Email == login.email && x.PasswordHash == passwordHash);
                if (result != null)
                {
                    Session[Common.Constans.Session.LOGIN_SESSION] = result;
                    if (login.remember)
                    {
                        Response.Cookies["email"].Value = result.Email;
                        Response.Cookies["email"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["password"].Value = result.PasswordHash;
                        Response.Cookies["password"].Expires = DateTime.Now.AddDays(30);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var hasEmail = _data.Users.Count(x => x.Email == model.email) > 0;
                if (hasEmail)
                    return Json(false, JsonRequestBehavior.AllowGet);
                else
                {
                    var role = _data.Roles.FirstOrDefault(x => x.RoleName != null && x.RoleName.Trim().ToLower() == Common.Constans.Role.Customer.Trim().ToLower());
                    User user = new User();
                    user.FullName = model.fullName;
                    user.PhoneNumber = model.phoneNumber;
                    user.Email = model.email;
                    var passwordHash = Encrypt.GetMD5(model.password);
                    user.PasswordHash = passwordHash;
                    user.RoleID = role.RoleID;
                    _data.Users.Add(user);
                    _data.SaveChanges();

                    var _user = _data.Users.Where(x => x.Email == model.email && x.PasswordHash == passwordHash).FirstOrDefault();
                    Session[Common.Constans.Session.LOGIN_SESSION] = _user;
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
                if (Session[Common.Constans.Session.LOGIN_SESSION] != null)
                {
                    var oldPass = Encrypt.GetMD5(model.oldPassword);
                    var user = _data.Users.Find(((User)Session[Common.Constans.Session.LOGIN_SESSION]).UserID);
                    if (user.PasswordHash == oldPass)
                    {
                        var newPass = Encrypt.GetMD5(model.newPassword);
                        user.PasswordHash = newPass;
                        _data.SaveChanges();
                        Session[Common.Constans.Session.LOGIN_SESSION] = user;
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
            var _user = _data.Users.Find(((User)Session["user"]).UserID);
            _user.LastActivity = now;
            _data.SaveChanges();
            Session.Remove(Common.Constans.Session.LOGIN_SESSION);
            return Redirect("/Home/Index");
        }

        public ActionResult Edit()
        {
            if (!(Session[Common.Constans.Session.LOGIN_SESSION] is User user))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User customer = _data.Users.FirstOrDefault(x => x.UserID == user.UserID);
            if (customer == null) return HttpNotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(User customer)
        {
            var userSession = Session[Common.Constans.Session.LOGIN_SESSION] as User;
            if (userSession == null)
            {
                return Json(new { status = false, message = "Bạn chưa đăng nhập" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                var user = _data.Users.FirstOrDefault(x => x.UserID == userSession.UserID);
                if (user != null)
                {
                    user.FullName = customer.FullName;
                    user.PhoneNumber = customer.PhoneNumber;
                    user.Email = customer.Email;
                    user.Province = customer.Province;
                    user.District = customer.District;
                    user.Ward = customer.Ward;
                    user.AddressDetail = customer.AddressDetail;
                    _data.SaveChanges();
                }
                return Json(new { status = true, message = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
        }
    }
}