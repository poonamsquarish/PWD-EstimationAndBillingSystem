using SQEstimationAndBillingSystem.Utils;
using System.Web.Optimization;

namespace SQEstimationAndBillingSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css",
            //          "~/admin-lte/css/AdminLTE.css",
            //          "~/admin-lte/css/skins/skin-blue.css"));

            //bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
            // "~/admin-lte/js/app.js"));


            #region style bundle
            bundles.Add(new StyleBundle(ProgConstant.strContent).Include(ProgConstant.sitecss,
                      ProgConstant.toastrcss,
                     ProgConstant.jqueryuicss,
                     "~/Content/jquery.auto-complete.css"
                     ));

            //bundles.Add(new StyleBundle(ProgConstant.cssApp).Include(ProgConstant.RAPcss, new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle(ProgConstant.adminlte).Include(
                ProgConstant.bootstrapmincss,
                ProgConstant.allskinsmincss,
                  ProgConstant.fontawesomemincss,
                  ProgConstant.ioniconsmincss,
                  ProgConstant.AdminLTEmincss,

                  ProgConstant.jqueryjvectormapcss,
                  ProgConstant.bootstrapdatepickermincss,
                  ProgConstant.daterangepickercss,
                  ProgConstant.wysihtml5mincss
                ));

            bundles.Add(new StyleBundle(ProgConstant.DataTables)
                    .Include(ProgConstant.bootstrapcss, new CssRewriteUrlTransform())
                    .Include("~/Content/DataTables/css/dataTables.bootstrap.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/DataTables/css/jquery.dataTables.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/DataTables/css/buttons.dataTables.min.css", new CssRewriteUrlTransform())
                     .Include("~/Content/DataTables/css/fixedHeader.dataTables.min.css", new CssRewriteUrlTransform())
                      .Include("~/Content/DataTables/css/rowGroup.dataTables.min.css", new CssRewriteUrlTransform()
                     )
                );

            bundles.Add(new StyleBundle(ProgConstant.select2).Include(ProgConstant.select2mincss));

            #endregion



            #region script Bundle
            bundles.Add(new ScriptBundle(ProgConstant.jquery).Include(
                ProgConstant.jqueryversion,
                       ProgConstant.jqueryui,
                       ProgConstant.jqueryblockUI,
                      ProgConstant.jquerycolorboxmin,
                       ProgConstant.bootboxmin,
                       "~/Scripts/jquery.auto-complete.min.js"
                       ));

            

            bundles.Add(new ScriptBundle(ProgConstant.jqueryval).Include(
                      ProgConstant.jqueryvalidate));

            bundles.Add(new ScriptBundle(ProgConstant.Unobtrusive_ajax).Include(
                ProgConstant.jqueryunobtrusive));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle(ProgConstant.jsmodernizr).Include(
                       ProgConstant.modernizr));

            bundles.Add(new ScriptBundle(ProgConstant.AjaxGlobalHandler).Include(
             ProgConstant.AjaxGlobalHandlerjs));


            bundles.Add(new ScriptBundle(ProgConstant.jsadminlte).Include(
                    ProgConstant.bootstrapmin,
                     ProgConstant.raphaelmin,
                    ProgConstant.jquerysparklinemin,
                     ProgConstant.jqueryjvectormap,
                    ProgConstant.jqueryjvectormapworldmillen,
                    ProgConstant.jqueryknobmin,
                    ProgConstant.momentmin,
                    ProgConstant.daterangepicker,
                      ProgConstant.slimscroll,
                   ProgConstant.bootstrapdatepickermin,
                   ProgConstant.bootstrap3wysihtml5,
                    ProgConstant.jqueryslimscroll,
                    ProgConstant.fastclick,
                    ProgConstant.adminltemin
                ));

            bundles.Add(new ScriptBundle(ProgConstant.jsDataTables).Include(
                    ProgConstant.jquerydataTables,
                   ProgConstant.dataTablesjqueryui,
                     ProgConstant.dataTablesbootstrap4min,
                   ProgConstant.jquerytabletojsonmin,
                    "~/Scripts/DataTables/jquery.jeditable.js",
                    "~/Scripts/DataTables/dataTables.buttons.min.js",
                    "~/Scripts/DataTables/jszip.min.js",
                     "~/Scripts/DataTables/pdfmake.min.js",
                     "~/Scripts/DataTables/vfs_fonts.js",
                    "~/Scripts/DataTables/buttons.html5.min.js",
                    "~/Scripts/DataTables/buttons.colVis.min.js",
                      "~/Scripts/DataTables/buttons.print.min.js",
                    "~/Scripts/DataTables/dataTables.select.min.js",
                    "~/Scripts/DataTables/dataTables.fixedHeader.min.js",
                    // "~/Scripts/DataTables/dataTables.rowGroup.min.js"
                    "~/Scripts/DataTables/dataTables.rowGroupV1.1.2.js"
                    ));

            bundles.Add(new ScriptBundle(ProgConstant.jstoastr).Include(
                     ProgConstant.toastr));

            bundles.Add(new ScriptBundle(ProgConstant.route).Include(
                 ProgConstant.appBase));

            bundles.Add(new ScriptBundle(ProgConstant.jsselect2).Include(
               ProgConstant.select2fullmin));


            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}
