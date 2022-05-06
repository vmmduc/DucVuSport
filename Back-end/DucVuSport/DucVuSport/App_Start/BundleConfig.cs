using System.Web;
using System.Web.Optimization;

namespace DucVuSport
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/myscript").Include(
                    "~/Scripts/js/event.js"));

            bundles.Add(new StyleBundle("~/Content/mycss").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/css/style.css",
                      "~/Content/css/home.css",
                      "~/Content/css/product-list.css",
                      "~/Content/css/product-detail.css",
                      "~/Content/css/cart-detail.css"));
        }
    }
}
