using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class Common_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {

            string cssPath = "~/bundles/Common_CSS";
            string jsPath = "~/bundles/Common_JS";

            List<string> cssList = new List<string>() {
                                                        //"~/css/angular-csp.css",
                                                        "~/css/common.min.css",
                                                        "~/css/table.min.css",
                                                        "~/css/page.min.css",
                                                        "~/css/list.min.css"
                                                      };

            List<string> jsList = new List<string>() {
                                                          "~/js/sys/menuPath.min.js"
                                                          ,"~/bower_components/jquery/dist/jquery.min.js"
                                                          ,"~/bower_components/angular/angular.min.js"

                                                     };
            cssList = cssList.FindAll(predicate);
            jsList = jsList.FindAll(predicate);

            bundles.Add(new StyleBundle(cssPath).Include(cssList.ToArray()));
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}