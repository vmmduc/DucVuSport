﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataAccessLib.DAO;
using DataAccessLib.Entities;
using DucVuSport.Models;

namespace DucVuSport.Controllers
{
    public class AccountController : Controller
    {
        private const string USER_SESSION = "user";
        // GET: Accont
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
                if (ModelState.IsValid)
                {
                    UserDAO user = new UserDAO();
                    var result = user.GetUser(model.email, model.passwd);
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
                UserDAO userDAO = new UserDAO();
                if (userDAO.CheckEmail(model.email))
                    ModelState.AddModelError("", "Email đã tồn tại");
                else
                {
                    tbUser user = new tbUser();
                    user.fullName = model.fullname;
                    user.email = model.email;
                    user.phoneNumber = model.phonenumber;
                    user.passwordHash = model.pwd;

                    var result = userDAO.Insert(user);
                    if (result > 0)
                    {
                        var _user = userDAO.GetUser(model.email, model.pwd);
                        Session[USER_SESSION] = _user;
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LogOut()
        {
            DateTime now = DateTime.Now; // Lấy thời gian hiện tại

            dataContext data = new dataContext();
            var _user = data.tbUsers.Find(((tbUser)Session["user"]).id);
            _user.lastActivity = now;
            data.SaveChanges();

            Session.Remove(USER_SESSION);
            return RedirectToAction("Index", "Home");
        }
    }
}