using System.Web;
using System.Web.Optimization;

namespace PA_System
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                       "~/Scripts/angular.js",
                       "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/site/modernizer").Include(
            "~/content/js/libs/modernizr-2.6.2.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/site/css").Include(
                      "~/site/bootstrap.css",
                      "~/site/site.css"));




            bundles.Add(new StyleBundle("~/site/css").Include(

                "~/Content/css/pace.css",

                "~/content/css/bootstrap.css",

                "~/content/css/libs/metisMenu.css",

                "~/content/css/sb-admin-2.css",

                "~/content/css/libs/multiple-select.css",

                "~/content/css/libs/font-awesome.css",

                "~/content/css/libs/datatables.css",

                "~/content/css/libs/animate.css",

                "~/content/css/site.css",

                "~/content/css/jquery.dataTables.min.css"
                ));


            bundles.Add(new ScriptBundle("~/site/js").Include(

                "~/content/js/blankfield.js",

                "~/content/js/libs/es6-promise.js",

                "~/content/js/libs/rx.all.js",

                "~/content/js/libs/jquery-2.1.4.js",

                "~/content/js/libs/moment.js",

                "~/content/js/libs/bootstrap.js",

                "~/content/js/libs/jquery.validate.js",

                "~/content/js/libs/metismenu.js",

                "~/content/js/libs/metisMenu.min.js",

                "~/content/js/libs/multiple-select.js",

                "~/content/js/sb-admin-2.js",

                "~/content/js/libs/datatables.js",

                "~/Scripts/jquery.js",

                "~/content/js/libs/jquery.dataTables.min.js",

                  "~/~/Scripts/jquery-1.12.3.js",

                 "~/content/js/libs/angular.js",

                "~/content/js/libs/angular-datatables.js",                 

                "~/content/js/libs/datetime-moment.js",

                "~/content/js/libs/jquery.noty.packaged.js",

                "~/content/js/libs/bootstrapnoty.js",

                "~/content/js/site.js"


                //"~/Content/js/libs/pace.js"

                ));
        }
    }
}
