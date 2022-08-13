using System.Web;
using System.Web.Optimization;

namespace Vidly
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Theme/vendor/datatables/datatables.bootstrap.js",
                        "~/Theme/vendor/jquery/jquery.min.js",
                        "~/Theme/vendor/bootstrap/js/bootstrap.bundle.min.js",
                        "~/Theme/vendor/bootstrap/js/bootstrap.js",
                        "~/Theme/vendor/datatables/jquery.dataTables.js",
                        "~/Theme/vendor/datatables/dataTables.bootstrap.js",
                        "~/Theme/vendor/datatables/dataTables.bootstrap4.min.css",
                        "~/Theme/vendor/jquery-easing/jquery.easing.min.js",
                        "~/Scripts/respond.js",
                        "~/scripts/typeahead.bundle.js",
                        "~/scripts/toastr.js"
                      ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap-lumen.css",
                       "~/Content/datatables/css/datatables.bootstrap.css",
                       "~/Content/typeahead.css",
                       "~/Content/toaster.css",
                       "~/Content/site.css"));
        }
    }
}
