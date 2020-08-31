using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class VaccinationDetail_Print_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/VaccinationDetail_Print_JS";
            List<string> jsList = new List<string>(){
                                                         "~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/Report/VaccinationM/VaccinationDetail_Print.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}