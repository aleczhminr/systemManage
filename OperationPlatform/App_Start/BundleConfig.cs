using System.Web;
using System.Web.Optimization;

namespace OperationPlatform
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/style/begin/core").Include(
                "~/css/core/pages-icons.css",
                "~/css/core/pages.min.css"
                ));
            


            bundles.Add(new StyleBundle("~/style/begin/plugins").Include(
                "~/css/plugins/boostrapv3/bootstrap.min.css",
                "~/css/plugins/font-awesome/font-awesome.min.css",
                "~/css/plugins/jquery-ui/jquery.scrollbar.css"
                ));
            //   图表库 使用 引入 
            bundles.Add(new StyleBundle("~/style/nvd3").Include(
                "~/css/plugins/nvd3/nv.d3.min.css"
                ));

            // form 表单  使用 引入 
            bundles.Add(new StyleBundle("~/style/form").Include(
                "~/css/plugins/boostrapv3/select2.css",
                "~/css/plugins/bootstrap-datepicker/datepicker3.css",
                "~/css/plugins/datatables-responsive/css/datatables.responsive.css",
                "~/css/plugins/jquery-ui/jquery.dataTables.min.css"
                ));


            //<!-- jQuery -->
            bundles.Add(new ScriptBundle("~/jquery").Include(
                      "~/Scripts/plugins/jquery/jquery.js",
                      "~/Scripts/plugins/jquery/jquery-easy.js"));
            bundles.Add(new ScriptBundle("~/jquery/ui").Include(
                      "~/Scripts/plugins/jquery-ui/jquery-ui.min.js",
                      "~/Scripts/plugins/jquery-ui/jquery.unveil.min.js",
                      "~/Scripts/plugins/jquery-ui/jquery.ioslist.min.js",
                      "~/Scripts/plugins/jquery-ui/jquery.actual.min.js",
                      "~/Scripts/plugins/jquery-ui/jquery.scrollbar.min.js",
                      "~/Scripts/plugins/jquery-autonumeric/autoNumeric.js"));

            //<!-- Modernizr -->  
            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                      "~/Scripts/plugins/modernizr.custom.js"));
            //BEGIN 主框架 CORE JS
            bundles.Add(new ScriptBundle("~/js/core").Include(
                      "~/Scripts/core/pages.min.js"));

            //<!-- Bootstrap -->
            bundles.Add(new ScriptBundle("~/js/boostrapv").Include(
                      "~/Scripts/plugins/boostrapv3/bootstrap.min.js",
                      "~/Scripts/plugins/boostrapv3/select2.min.js"));


            //<!-- 表单 & 表格 使用引入 -->
            bundles.Add(new ScriptBundle("~/js/begin/form").Include(
                      "~/Scripts/plugins/jquery-ui/jquery.dataTables.min.js",
                      "~/Scripts/plugins/datatables-responsive/datatables.responsive.js",
                      "~/Scripts/plugins/datatables-responsive/lodash.min.js",
                      "~/Scripts/plugins/bootstrap-datepicker/bootstrap-datepicker.js",
                      "~/Scripts/plugins/classie/classie.js"));
            //<!-- 图表库 使用 引入 -->
            bundles.Add(new ScriptBundle("~/js/nvd3").Include(
                      "~/Scripts/plugins/d3/d3.min.js",
                      "~/Scripts/plugins/nvd3/d3.v3.js",
                      "~/Scripts/plugins/nvd3/nv.d3.min.js"));
            // <!-- Gauge Charts 使用引入 -->
            bundles.Add(new ScriptBundle("~/js/gaugeCharts").Include(
                      "~/Scripts/plugins/gaugeCharts/globalize.min.js",
                      "~/Scripts/plugins/gaugeCharts/dx.chartjs.js"));
            // 表格 使用引入
            bundles.Add(new ScriptBundle("~/js/tables").Include(
                      "~/Scripts/tables.js"));

        }
    }
}
