using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class BatchSetting_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/BatchSetting_JS";
            List<string> jsList = new List<string>(){
                                                         "~/js/sys/menuPath.min.js"
                                                        ,"~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/js/ang/FilterM.min.js"
                                                        ,"~/js/ang/ToolM.min.js"
                                                        ,"~/Vaccination/ParameterM/BatchSetting.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}