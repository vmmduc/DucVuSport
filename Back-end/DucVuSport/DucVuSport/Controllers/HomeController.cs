using DucVuSport.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ssport.Controllers
{
    public class HomeController : Controller
    {
        private readonly dataContext _data = new dataContext();

        public ActionResult Index()
        {
            ViewBag.TopProduct = _data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
            return View();
        }

        // Create Customer
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(_data.Accounts, "AccountID", "Email");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            var user = Session["user"] as DucVuSport.Models.Entities.Account;
            if (ModelState.IsValid)
            {
                customer.AccountID = user.AccountID;
                _data.Customers.Add(customer);
                _data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(_data.Accounts, "AccountID", "Email", customer.AccountID);
            return View(customer);
        }


        // Edit infor
        [Route("Edit")]
        public ActionResult Edit()
        {
            Account user = Session["user"] as DucVuSport.Models.Entities.Account;
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _data.Customers.FirstOrDefault(x=>x.AccountID == user.AccountID);
            if (customer == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            var user = Session["user"] as DucVuSport.Models.Entities.Account;
            if(user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                customer = _data.Customers.FirstOrDefault(x => x.AccountID == user.AccountID);
                _data.Entry(customer).State = EntityState.Modified;
                _data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}