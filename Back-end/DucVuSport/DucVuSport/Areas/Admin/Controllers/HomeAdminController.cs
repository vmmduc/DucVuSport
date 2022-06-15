using DucVuSport.Models.Entities;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace DucVuSport.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    public class HomeAdminController : Controller
    {
        private readonly DataContext data = new DataContext();
        public ActionResult Index()
        {
            var waitingConfirm = data.OrderStatus.FirstOrDefault(x=>x.Name != null && x.Name.Trim().ToLower() == Common.Constans.Status.WaitingConfirm);
            ViewBag.viewTotal = data.Products.Sum(x => x.ViewCount);
            ViewBag.top10 = data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            ViewBag.totalOrder = data.Orders.Count(x => x.Status == 1 || x.Status == 2);
            var order = data.Orders.Include(o => o.OrderStatu).Include(o => o.Payment).Include(o => o.User).Where(x => x.Status == 1 || x.Status == 2).ToList();
            return View(order);
        }
        public ActionResult OrderDetail(int id)
        {
            var order = data.Orders.Include(x => x.OrderStatu).FirstOrDefault(x => x.OrderID == id);
            var customer = data.Users.FirstOrDefault(x => x.UserID == order.CustomerID);
            var orderDetails = data.OrderDetails.Include(o => o.Order).Include(o => o.Product).Where(x => x.OrderID == id);
            ViewBag.order = order;
            ViewBag.customer = customer;
            ViewBag.Status = new SelectList(data.OrderStatus, "ID", "Name", order.Status);
            return View(orderDetails.ToList());
        }
        public ActionResult Confirm(int id)
        {
            var order = data.Orders.Where(x => x.OrderID == id).FirstOrDefault();
            if (order != null)
            {
                order.Status = 2;
                data.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}