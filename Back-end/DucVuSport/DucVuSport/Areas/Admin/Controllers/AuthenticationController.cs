using DucVuSport.Models;
using DucVuSport.Models.Entities;
using System.Linq;
using System.Web.Mvc;

namespace DucVuSport.Areas.Admin.Controllers
{
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
                var passwordHash = Utilities.Encrypt.GetMD5(model.password);
                var user = _data.Users.FirstOrDefault(x => x.Email == model.email && x.PasswordHash == passwordHash);
                if (user != null)
                {
                    Session[Common.Constans.Session.ADMIN_SESSION] = user;
                    return RedirectToAction("Index", "HomeAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng");
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            if (Session[Common.Constans.Session.ADMIN_SESSION] != null)
                Session.Remove(Common.Constans.Session.ADMIN_SESSION);
            return RedirectToAction("Login");
        }
        #endregion
    }
}

