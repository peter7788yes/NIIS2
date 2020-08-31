using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class StudentRecord_Upload_Bundle
{

		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/StudentRecord_Upload_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.min.js"
                                                        ,"~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/js/ang/PageM.min.js"
                                                        ,"~/js/ang/FilterM.min.js"
                                                        ,"~/Vaccination/RecordM/StudentRecord_Upload.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}