using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PAET.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/wizard").Include(
            "~/Scripts/jquery.bootstrap.wizard.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            //bundles.Add(new ScriptBundle("~/bundles/prism").Include(
            //          "~/Scripts/prism.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/codeflask").Include(
            //           "~/Scripts/codeflask.js",
            //           "~/Scripts/codeflask-editor.js"));
            bundles.Add(new ScriptBundle("~/bundles/codemirror").Include(
          "~/Content/CodeMirror/lib/codemirror.js",
          "~/Content/CodeMirror/mode/clike.js"
          ));
            bundles.Add(new ScriptBundle("~/bundles/editarea").Include(
            "~/Scripts/edit_area_full.js"
            ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-dialog.css",
                       "~/Content/site.css",
                       "~/Content/CodeMirror/lib/codemirror.css",
                       "~/Content/CodeMirror/theme/eclipse.css",
                       "~/Content/chosen.css",
                       "~/Content/chosen-bootstrap.css", 
                       "~/Content/bootstrap-rating.css",
                       "~/Content/font-awesome.min.css"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
            "~/Scripts/chosen.jquery.js"));
            bundles.Add(new ScriptBundle("~/bundles/confirm").Include(
            "~/Scripts/bootstrap-dialog.js"));
            bundles.Add(new ScriptBundle("~/bundles/rating").Include(
            "~/Scripts/bootstrap-rating/bootstrap-rating.min.js"));
        }
    }
}