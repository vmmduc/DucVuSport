using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DucVuSport.Models.Entities;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.OrderStatu).Include(o => o.Payment).Include(o => o.User);
            return View(orders.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}
