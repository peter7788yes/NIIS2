using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class LogM_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string cssPath = "~/bundles/LogM_CSS";
            string jsPath = "~/bundles/LogM_JS";

            List<string> cssList = new List<string>();
            List<string> jsList = new List<string>(){
                                    "~/js/jq/jquery-2.1.4.js"
                                  , "~/js/jq/url.js"
                                  , "~/js/ang/angular-1.3.16.js"
                                  , "~/js/ang/angular-sanitize-1.3.16.js"
                                  , "~/js/ang/FilterM.js"
                                  , "~/js/ang/InputM.js"
                                  , "~/js/ang/PageM.js"
                                  , "~/js/ang/TableM.js"
                                  , "~/LogM/Log.js"
                                 };

            cssList = cssList.FindAll(predicate);
            jsList = jsList.FindAll(predicate);

            bundles.Add(new StyleBundle(cssPath).Include(cssList.ToArray()));

            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}