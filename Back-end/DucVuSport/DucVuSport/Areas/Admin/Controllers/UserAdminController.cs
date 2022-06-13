using DucVuSport.Models;
using DucVuSport.Models.Entities;
using DucVuSport.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        private readonly dataContext _data = new dataContext();
        public const string SESSION_ADMIN = "session_admin";
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
         public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string passwordHash = Encrypt.GetMD5(model.password);
                User account = _data.Users.FirstOrDefault(x=>x.Email == model.email && x.PasswordHash == model.password);
                if (account != null)
                {
                    Session[SESSION_ADMIN] = account;
                }
            }
            return View();
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