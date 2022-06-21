using DucVuSport.Common;
using DucVuSport.Models;
using DucVuSport.Models.Entities;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace DucVuSport.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _data = new DataContext();

        #region Authentication
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = Encrypt.GetMD5(model.password.Trim().ToLower());
                var user = _data.Users.FirstOrDefault(x => x.Email.Trim().ToLower() == model.email.Trim().ToLower() && x.PasswordHash == passwordHash);
                if (user != null)
                {
                    var role = _data.Roles.FirstOrDefault(x => x.RoleName != null && x.RoleName.Trim().ToLower() == Common.Constans.Role.CUSTOMER);
                    if (user.IsDeleted == true)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        return View("Login");
                    }
                    else if (user.Unlock == false)
                    {
                        ModelState.AddModelError("", "Tài khoản đã bị khóa, vui lòng liên hệ với quản trị viên");
                        return View("Login");
                    }
                    else if (user.RoleID == role.RoleID)
                    {
                        ModelState.AddModelError("", "Bạn không có quyền đăng nhập vào trang này");
                        return View("Login");
                    }
                    else
                    {
                        Session[Constans.Session.ADMIN_SESSION] = user;
                        FormsAuthentication.SetAuthCookie(user.UserID.ToString(), false);
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                    return View("Login");
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            if (Session[Common.Constans.Session.ADMIN_SESSION] != null)
            {
                var account = Session[Common.Constans.Session.ADMIN_SESSION] as DucVuSport.Models.Entities.User;
                account.LastActivity = DateTime.Now;
                _data.SaveChanges();
                Session.Remove(Common.Constans.Session.ADMIN_SESSION);
            }
            return RedirectToAction("Login");
        }
        #endregion
    }
}

