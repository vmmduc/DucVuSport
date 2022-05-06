using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLib.DAO;
using DucVuSport.Models;

namespace DucVuSport.Controllers
{
    public class AccountController : Controller
    {
        // GET: Accont
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDAO user = new UserDAO();
                var result = user.getUser(model.email, model.passwd);
                if (result != null)
                {
                    Session["user"] = result;
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
    }
}