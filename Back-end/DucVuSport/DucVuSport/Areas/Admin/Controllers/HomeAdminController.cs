using DucVuSport.Models.Entities;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace DucVuSport.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    public class HomeAdminController : BaseController
    {
        private readonly DataContext data = new DataContext();
        public ActionResult Index()
        {
            var waitingConfirm = data.OrderStatus.FirstOrDefault(x => x.Name != null && x.Name.Trim().ToLower() == Common.Constans.Status.WaitingConfirm.Trim().ToLower());
            var approve = data.OrderStatus.FirstOrDefault(x => x.Name != null && x.Name.Trim().ToLower() == Common.Constans.Status.Approve.Trim().ToLower());
            var success = data.OrderStatus.FirstOrDefault(x => x.Name != null && x.Name.Trim().ToLower() == Common.Constans.Status.Success.Trim().ToLower());

            ViewBag.turnover = data.Orders.Where(x => x.Status == success.ID).Sum(x => x.Total);
            ViewBag.viewTotal = data.Products.Sum(x => x.ViewCount);
            ViewBag.top10 = data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            ViewBag.totalOrder = data.Orders.Count(x => x.Status == waitingConfirm.ID || x.Status == approve.ID);
            var order = data.Orders.Include(o => o.OrderStatu).Include(o => o.Payment).Include(o => o.User).Where(x => x.Status != success.ID).ToList();
            return View(order);
        }
        public ActionResult OrderDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = data.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var customer = data.Users.FirstOrDefault(x => x.UserID == order.CustomerID);
            var orderDetails = data.OrderDetails.Include(o => o.Order).Include(o => o.Product).Where(x => x.OrderID == id);
            ViewBag.customer = customer;
            ViewBag.status = new SelectList(data.OrderStatus, "ID", "Name", order.Status);
            ViewBag.orderDetail = orderDetails.ToList();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderDetail(Order order)
        {
            if (ModelState.IsValid)
            {
                data.Entry(order).State = EntityState.Modified;
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
    }
}