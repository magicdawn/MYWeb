using System.Web;
using System.Web.Optimization;

namespace MYWeb
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jquery
            bundles.Add(new ScriptBundle(
                "~/jquery",//virtual path
                "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.min.js" //cdn path
            ).Include("~/Scripts/jquery-{version}.js"));

            //jquery validate
            bundles.Add(new ScriptBundle("~/jquery-validate").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"
            ));

            //bootstrap js css
            bundles.Add(new ScriptBundle("~/bootstrap-js").Include(
                "~/Content/Bootstrap/bootstrap.js"
            ));
            bundles.Add(new StyleBundle("~/bootstrap-css").Include(
                "~/Content/Bootstrap/bootstrap.css",
                "~/Content/Bootstrap/bootstrap-theme.css"
            ));


            //布局
            bundles.Add(new StyleBundle("~/layout-css").Include("~/Content/layout.css"));

            //bootstrap v2 js & css
            bundles.Add(new StyleBundle("~/bootstrap-v2-css").Include(
                "~/Content/Bootstrap/v2/bootstrap.css"
                //响应式布局
                //"~/Content/Bootstrap/v2/bootstrap-responsive.css"
            ));
            bundles.Add(new ScriptBundle("~/bootstrap-v2-js").Include(
                "~/Content/Bootstrap/v2/bootstrap.js"
            ));
            /*
             * 约定以中划线分割
             * jquery
             * jquery-ui
             * jquery-ui-balck-theme
             * 
             * jquery-validate
             */
        }
    }
}