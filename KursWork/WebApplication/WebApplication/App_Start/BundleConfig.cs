using System.Web;
using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            try
            {
                bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            }
            catch { }
            //try {

                bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                            "~/Scripts/jquery.validate*"));
            //}
            //catch { }

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            try {
                bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                "~/Scripts/modernizr-*"));
            }
            catch { }
            try {
                bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                          "~/Scripts/bootstrap.js",
                          "~/Scripts/respond.js"));
            }
            catch { }
            try
            { 
                bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css"));

                bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                          "~/Content/themes/base/tabs.css",
                          "~/Content/themes/base/accordion.css"));
            }
            catch { }
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include("~/Content/themes/base/all.css"));
        }
    }
}
