using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class OrgManagement_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/OrgManagement_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.min.js"
                                                        ,"~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/bower_components/lodash/dist/lodash.min.js"
                                                        ,"~/js/ang/TreeM.min.js"
                                                        ,"~/System/OrgM/OrgManagement.min.js"
                                                   };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));
        }
}