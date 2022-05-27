using DucVuSport.Models;
using DucVuSport.Models.Entities;
using DucVuSport.Utilities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ssport.Controllers
{
    public class HomeController : Controller
    {
        private dataContext _data = new dataContext();

        public ActionResult Index()
        {
            return View(_data.Products.OrderByDescending(x => x.Sold).Take(10).ToList());
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
            var user = Session["user"] as DucVuSport.Models.Entities.Account;
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