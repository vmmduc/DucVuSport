using System.Web;
using System.Web.Optimization;

namespace DucVuSport
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-{version}.min.js",
                       "~/Scripts/jquery.validate.min.js",
                       "~/Scripts/jquery.validate.unobtrusive.min.js",
                       "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/toast").Include(
                        "~/Script/toastr.js"));

            bundles.Add(new StyleBundle("~/bundles/style-toast").Include(
                        "~/Content/toastr.css"));

            bundles.Add(new ScriptBundle("~/bundles/myscript").Include(
                        "~/Scripts/js/event.js"));

            bundles.Add(new StyleBundle("~/bundles/mycss").Include(
                          "~/Content/bootstrap.min.css",
                          "~/Content/css/style.css",
                          "~/Content/css/home.css",
                          "~/Content/css/product-list.css",
                          "~/Content/css/product-detail.css",
                          "~/Content/css/cart-detail.css"));
        }
    }
}
