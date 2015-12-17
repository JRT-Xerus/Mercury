using System.Web;
using System.Web.Optimization;

namespace Mercury
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-1.11.4.js"));
            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
            //          "~/Scripts/gridmvc.js"));

            bundles.Add(new ScriptBundle("~/bundles/pqgrid").Include(
                      "~/Scripts/pqgrid.dev.js"));

            bundles.Add(new ScriptBundle("~/bundles/paulund_modal_box").Include(
                      "~/Scripts/jquery.paulund_modal_box.js"));

            bundles.Add(new ScriptBundle("~/bundles/Mercury").Include(
                      "~/Scripts/Mercury.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Scripts/pqgrid.dev.css"));

            //"~/Content/Gridmvc.css",
            //          "~/Content/gridmvc.datepicker.css",
        }
    }
}
