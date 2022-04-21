using System.Web.Optimization;

namespace StoreFront.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/vendor/jquery/jquery.min.js",
                        "~/Content/assets/js/custom.js",
                      "~/Content/assets/js/owl.js",
                      "~/Content/assets/js/slick.js",
                      "~/Content/assets/js/isotope.js",
                      "~/Content/assets/js/accordions.js"
                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));



            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jQuery-3.3.1.min.js",
                      "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Content/vendor/bootstrap/js/bootstrap.min.js",
                      "~/Scripts/DataTables/jquery.datatables.min.js"///Datatables
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/content/css/bootstrap.min.css",
                      "~/Content/assets/css/fontawesome.css",
                      "~/Content/assets/css/templatemo-sixteen.css",
                      "~/Content/assets/css/owl.css",
                      "~/Content/PagedList.css",//PageListing
                      "~/Content/DataTables/css/dataTables.jqueryui.min.css"//Datatables

                      ));
        }
    }
}
