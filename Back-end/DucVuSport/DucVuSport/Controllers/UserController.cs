using DucVuSport.Models;
using DucVuSport.Models.Entities;
using DucVuSport.Utilities;
using Facebook;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace DucVuSport.Controllers
{
    public class UserController : Controller
    {
        private dataContext data = new dataContext();
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
                var result = data.Accounts.Where(x => x.Email == login.email && x.PasswordHash == passwordHash).FirstOrDefault();
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
                var hasEmail = data.Accounts.Count(x => x.Email == model.email) > 0;
                if (hasEmail)
                    return Json(false, JsonRequestBehavior.AllowGet);
                else
                {
                    Account user = new Account();
                    user.Email = model.email;
                    var passwordHash = Encrypt.GetMD5(model.password);
                    user.PasswordHash = passwordHash;

                    data.Accounts.Add(user);
                    data.SaveChanges();

                    var _user = data.Accounts.Where(x => x.Email == model.email && x.PasswordHash == passwordHash).FirstOrDefault();
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
                    var user = data.Accounts.Find(((Account)Session["user"]).AccountID);
                    if (user.PasswordHash == oldPass)
                    {
                        var newPass = Encrypt.GetMD5(model.newPassword);
                        user.PasswordHash = newPass;
                        data.SaveChanges();
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
            var _user = data.Accounts.Find(((Account)Session["user"]).AccountID);
            _user.LastActivity = now;
            data.SaveChanges();
            Session.Remove(USER_SESSION);
            return Redirect("/Home/Index");
        }



        private Uri RederectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallBack");
                return uriBuilder.Uri;
            }
        }

        [Route("Facebook")]
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RederectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                rederect_url = RederectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            return Redirect("/");
        }
    }
}