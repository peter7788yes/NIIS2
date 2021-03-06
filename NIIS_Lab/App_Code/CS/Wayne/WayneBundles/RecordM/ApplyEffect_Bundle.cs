﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class ApplyEffect_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/ApplyEffect_JS";
            List<string> jsList = new List<string>(){
                                                        "~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/js/ang/FilterM.min.js"
                                                        ,"~/Vaccination/RecordM/ApplyEffect.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}