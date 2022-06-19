using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DucVuSport.Models.Entities;
using Rotativa;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class BillController : BaseController
    {
        private DataContext _data = new DataContext();
        public ActionResult Index()
        {
            var orders = _data.Orders.Include(o => o.OrderStatu).Include(o => o.Payment).Include(o => o.User);
            return View(orders.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _data.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var customer = _data.Users.FirstOrDefault(x => x.UserID == order.CustomerID);
            var orderDetails = _data.OrderDetails.Include(o => o.Order).Include(o => o.Product).Where(x => x.OrderID == id);
            ViewBag.customer = customer;
            ViewBag.status = new SelectList(_data.OrderStatus, "ID", "Name", order.Status);
            ViewBag.orderDetail = orderDetails.ToList();
            return View(order);
        }

        public ActionResult PrintPDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _data.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var customer = _data.Users.FirstOrDefault(x => x.UserID == order.CustomerID);
            var orderDetails = _data.OrderDetails.Include(x => x.Order).Include(x => x.Product).Where(x => x.OrderID == id);
            ViewBag.customer = customer;
            ViewBag.status = new SelectList(_data.OrderStatus, "ID", "Name", order.Status);
            ViewBag.orderDetail = orderDetails.ToList();
            return new PartialViewAsPdf("_PrintPDF", order)
            {
                PageOrientation = Rotativa.Options.Orientation.Landscape,
                PageSize = Rotativa.Options.Size.A4,
                FileName = order.OrderID + "_" + customer.FullName + "_Bill.pdf"
            };
        }
    }
}
