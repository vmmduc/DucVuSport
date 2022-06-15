using DucVuSport.Models.Entities;
using System.Web.Mvc;
using System.Web.Routing;

namespace DucVuSport.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[Common.Constans.Session.ADMIN_SESSION] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "UserAdmin", action = "Login", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}