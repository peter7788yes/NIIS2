using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class AbnormalVaccinationDetail_Print_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/AbnormalVaccinationDetail_Print_JS";
            List<string> jsList = new List<string>(){
                                                        "~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/Report/VaccinationM/AbnormalVaccinationDetail_Print.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}