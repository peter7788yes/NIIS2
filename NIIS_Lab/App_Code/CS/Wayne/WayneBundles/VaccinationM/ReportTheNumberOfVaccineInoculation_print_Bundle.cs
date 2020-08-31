using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class ReportTheNumberOfVaccineInoculation_Print_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/ReportTheNumberOfVaccineInoculation_Print_JS";
            List<string> jsList = new List<string>(){
                                                        "~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/Report/VaccinationM/ReportTheNumberOfVaccineInoculation_Print.min.js"
                                                    };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}