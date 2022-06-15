using DucVuSport.Models;
using DucVuSport.Models.Entities;
using System.Linq;
using System.Web.Mvc;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        private readonly DataContext _data = new DataContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var passwordHash = Utilities.Encrypt.GetMD5(model.password);
            var user = _data.Users.FirstOrDefault(x => x.Email == model.email && x.PasswordHash == passwordHash);
            if (user != null)
            {
                Session[Common.Constans.Session.ADMIN_SESSION] = user;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(User account)
        {
            return View();
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}