﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class UC_SelectAgency_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/UC_SelectAgency_JS";
            List<string> jsList = new List<string>(){
                                                        "~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/js/ang/hotkeys.min.js"
                                                        ,"~/js/ang/PageM.min.js"
                                                        ,"~/UC/UC_SelectAgency.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}