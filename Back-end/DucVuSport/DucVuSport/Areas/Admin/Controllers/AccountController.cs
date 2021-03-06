using DucVuSport.Common;
using DucVuSport.Models;
using DucVuSport.Models.Entities;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _data = new DataContext();

        [Authorize(Roles = Constans.Role.ADMIN)]
        public ActionResult Index()
        {
            var users = _data.Users.Where(x => x.PasswordHash != null && x.IsDeleted != true);
            ViewBag.RoleID = new SelectList(_data.Roles, "RoleID", "RoleName");
            return View(users.ToList());
        }


        [Authorize(Roles = Constans.Role.ADMIN)]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _data.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(_data.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        #region Create account
        [Authorize(Roles = Constans.Role.ADMIN)]
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(_data.Roles, "RoleID", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constans.Role.ADMIN)]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Common.Encrypt.GetMD5(user.Email);
                user.PasswordHash = passwordHash;
                _data.Users.Add(user);
                _data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(_data.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }
        #endregion

        #region Update account
        public ActionResult Update()
        {
            if (!(Session[Constans.Session.ADMIN_SESSION] is User user))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User customer = _data.Users.FirstOrDefault(x => x.UserID == user.UserID);
            if (customer == null) return HttpNotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(User account)
        {
            var userSession = Session[Constans.Session.ADMIN_SESSION] as User;
            if (userSession == null)
            {
                return Json(new { status = false, message = "Bạn chưa đăng nhập" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                var user = _data.Users.FirstOrDefault(x => x.UserID == userSession.UserID);
                if (user != null)
                {
                    user.FullName = account.FullName;
                    user.PhoneNumber = account.PhoneNumber;
                    user.Province = account.Province;
                    user.District = account.District;
                    user.Ward = account.Ward;
                    user.AddressDetail = account.AddressDetail;
                    _data.SaveChanges();
                }
                return Json(new { status = true, message = "Cập nhật thông tin thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Change password
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            var session = Session[Constans.Session.ADMIN_SESSION] as User;
            if (ModelState.IsValid)
            {
                if (session != null)
                {
                    var oldPass = Encrypt.GetMD5(model.oldPassword.Trim().ToLower());
                    var user = _data.Users.Find(session.UserID);
                    if (user.PasswordHash != oldPass)
                    {
                        return Json(new { status = true, message = "Mật khẩu cũ không đúng" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var newPass = Encrypt.GetMD5(model.newPassword.Trim().ToLower());
                        user.PasswordHash = newPass;
                        _data.SaveChanges();
                        Session[Common.Constans.Session.ADMIN_SESSION] = user;
                        return Json(new { status = true, message = "Thay đổi thành công" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { status = false, message = "Thay đổi thất bại" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [Authorize(Roles = Constans.Role.ADMIN)]
        public JsonResult ChangeRole(User account)
        {
            var user = _data.Users.Find(account.UserID);
            if (user == null)
            {
                return Json(new { status = false, message = "Không tìm thấy tài khoản" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                user.RoleID = account.RoleID;
                _data.SaveChanges();
                return Json(new { status = true, message = "Đã cấp quyền " + user.Role.RoleName + " cho tài khoản" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constans.Role.ADMIN)]
        public JsonResult ResetPassword(int? id)
        {
            if (id == null)
            {
                return Json(new { status = false, message = "Lỗi!" }, JsonRequestBehavior.AllowGet);
            }
            var user = _data.Users.Find(id);
            if (user == null)
            {
                return Json(new { status = false, message = "Không tìm thấy tài khoản!" }, JsonRequestBehavior.AllowGet);
            }
            var passwordHash = Encrypt.GetMD5(user.Email.Trim().ToLower());
            user.PasswordHash = passwordHash;
            _data.SaveChanges();
            return Json(new { status = true, message = "Đã đặt lại mật khẩu!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = Constans.Role.ADMIN)]
        public JsonResult LockedStaus(int? id)
        {
            if (id == null)
            {
                return Json(new { status = false, message = "Lỗi!" }, JsonRequestBehavior.AllowGet);
            }
            var user = _data.Users.Find(id);
            if (user == null)
            {
                return Json(new { status = false, message = "Không tìm thấy tài khoản!" }, JsonRequestBehavior.AllowGet);
            }
            user.Unlock = (user.Unlock == true ? false : true);
            _data.SaveChanges();
            if (!user.Unlock)
            {
                return Json(new { status = true, message = "Đã mở khóa tài khoản!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = true, message = "Đã khóa tài khoản!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = Constans.Role.ADMIN)]
        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { status = false, message = "Lỗi!" }, JsonRequestBehavior.AllowGet);
            }
            var role = _data.Roles.FirstOrDefault(x => x.RoleName.Trim().ToLower() == Constans.Role.ADMIN);
            var user = _data.Users.Find(id);
            if (user == null)
            {
                return Json(new { status = false, message = "Không tìm thấy tài khoản!" }, JsonRequestBehavior.AllowGet);
            }
            else if (user.RoleID == role.RoleID)
            {
                return Json(new { status = false, message = "Không thể xóa tài khoản " + role.RoleName }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                user.IsDeleted = true;
                _data.SaveChanges();
                return Json(new { status = true, message = "Xóa thành công!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}