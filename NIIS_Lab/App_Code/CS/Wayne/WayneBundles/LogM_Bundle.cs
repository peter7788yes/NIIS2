using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class LogM_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string cssPath = "~/bundles/LogM_CSS";
            string jsPath = "~/bundles/LogM_JS";

            List<string> cssList = new List<string>();
            List<string> jsList = new List<string>(){
                                  "~/bower_components/jquery/dist/jquery.min.js"
                                  ,"~/bower_components/angular/angular.min.js"
                                  , "~/js/jq/url.min.js"
                                  , "~/js/ang/angular-sanitize-1.3.16.min.js"
                                  , "~/js/ang/FilterM.min.js"
                                  , "~/js/ang/InputM.min.js"
                                  , "~/js/ang/PageM.min.js"
                                  , "~/js/ang/TableM.min.js"
                                  , "~/LogM/Log.min.js"
                                 };

            cssList = cssList.FindAll(predicate);
            jsList = jsList.FindAll(predicate);

            bundles.Add(new StyleBundle(cssPath).Include(cssList.ToArray()));

            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}