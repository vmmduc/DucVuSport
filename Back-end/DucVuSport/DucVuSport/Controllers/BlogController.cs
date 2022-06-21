using DucVuSport.Models.Entities;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;

namespace DucVuSport.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext = new DataContext();
        [Route("Blog")]
        public ActionResult Index()
        {
            var blogList = _dataContext.Blogs.ToList();
            return View(blogList);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog = _dataContext.Blogs.Include(b => b.User).Where(x=>x.BlogID == id).FirstOrDefault();
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.category = _dataContext.BlogCategories.ToList();
            return View(blog);
        }
    }
}