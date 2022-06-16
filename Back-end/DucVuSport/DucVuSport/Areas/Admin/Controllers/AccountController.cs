using DucVuSport.Models.Entities;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;

namespace DucVuSport.Areas.Admin.Controllers
{
/*
 * Thay đổi quyền truy cập
 * Khóa / mở khóa tài khoản
 */
    public class AccountController : BaseController
    {
        private readonly DataContext _data = new DataContext();
        public ActionResult Index()
        {
            var users = _data.Users.Where(x=>x.PasswordHash != null && x.IsDeleted != true).Include(x => x.Role);
            return View(users.ToList());
        }

        #region Create account
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(_data.Roles, "RoleID", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
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
            if (!(Session[Common.Constans.Session.ADMIN_SESSION] is User user))
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
            var userSession = Session[Common.Constans.Session.ADMIN_SESSION] as User;
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
                    user.Email = account.Email;
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

        public ActionResult Delete(int? id)
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
            user.IsDeleted = true;
            _data.SaveChanges();
            return RedirectToAction("Index", "Account");
        }
    }
}