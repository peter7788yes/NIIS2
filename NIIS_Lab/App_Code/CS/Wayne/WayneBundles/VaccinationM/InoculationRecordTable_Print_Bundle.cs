﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class InoculationRecordTable_Print_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/InoculationRecordTable_Print_JS";
            List<string> jsList = new List<string>(){
                                                        "~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/js/ang/FilterM.min.js"
                                                        ,"~/Report/VaccinationM/InoculationRecordTable_Print.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}