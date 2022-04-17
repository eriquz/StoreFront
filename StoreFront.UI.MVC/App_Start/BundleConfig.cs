using System.Web.Optimization;

namespace StoreFront.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/vendor/jquery/jquery.min.js",
                        "~/Content/assets/js/custom.js",
                      "~/Content/assets/js/owl.js",
                      "~/Content/assets/js/slick.js",
                      "~/Content/assets/js/isotope.js",
                      "~/Content/assets/js/accordions.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));



            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/assets/css/fontawesome.css",
                      "~/Content/assets/css/templatemo-sixteen.css",
                      "~/Content/assets/css/owl.css"
                      ));
        }
    }
}
