using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class Common_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {

            string cssPath = "~/bundles/Common_CSS";
            string jsPath = "~/bundles/Common_JS";

            List<string> cssList = new List<string>() {
                                                        //"~/css/angular-csp.css",
                                                        "~/css/common.css",
                                                        "~/css/table.css",
                                                        "~/css/page.css",
                                                        "~/css/list.css"
                                                      };

            List<string> jsList = new List<string>() {
                                                          "~/js/sys/menuPath.js"
                                                          ,"~/js/jq/jquery-2.1.4.js"
                                                          ,"~/js/ang/angular-1.4.3.js"
                                                          
                                                     };
            cssList = cssList.FindAll(predicate);
            jsList = jsList.FindAll(predicate);

            bundles.Add(new StyleBundle(cssPath).Include(cssList.ToArray()));
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}