using System.Web;
using System.Web.Optimization;

namespace Sustan
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery.min.js",
                        "~/Scripts/AdminMenu.js",
                        "~/scripts/datatables/jquery.datatables.js",
                        "~/scripts/datatables/datatables.bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/theme/scripts").Include(
                        "~/Scripts/tether.min.js",
                        "~/Scripts/jquery.easing.js",
                        "~/Scripts/jquery-waypoints.js",
                        "~/scripts/jquery-validate.js",
                        "~/Scripts/jquery.cookie.js",

                        "~/Scripts/owl.carousel.js",
                        "~/Scripts/jquery.flexslider-min.js",
                        "~/Scripts/headline.js",
                        "~/Scripts/parallax.js",

                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
                        "~/Content/revolution/js/jquery.themepunch.tools.min.js",
                        "~/Content/revolution/js/jquery.themepunch.revolution.min.js",
                        "~/Content/revolution/js/slider.js"));

            bundles.Add(new ScriptBundle("~/bundles/slider/extensions").Include(
                        "~/Content/revolution/js/extensions/revolution.extension.actions.min.js",
                        "~/Content/revolution/js/extensions/revolution.extension.carousel.min.js",
                        "~/Content/revolution/js/extensions/revolution.extension.kenburn.min.js",
                        "~/Content/revolution/js/extensions/revolution.extension.layeranimation.min.js",
                        "~/Content/revolution/js/extensions/revolution.extension.migration.min.js",
                        "~/Content/revolution/js/extensions/revolution.extension.navigation.min.js",
                        "~/Content/revolution/js/extensions/revolution.extension.parallax.min.js",
                        "~/Content/revolution/js/extensions/revolution.extension.slideanims.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/content/datatables/css/datatables.bootstrap.css",
                      "~/Content/footer.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/content/bootstrap.css",
                      "~/content/datatables/css/datatables.bootstrap.css",
                      "~/css/style.css",
                      "~/css/colors/color4.css",
                      "~/css/headline.css",
                      "~/css/responsive.css",
                      "~/css/animate.css",
                      "~/Content/site.css",
                      "~/Content/revolution/css/layers.css",
                      "~/Content/revolution/css/settings.css"));
        }
    }
}
