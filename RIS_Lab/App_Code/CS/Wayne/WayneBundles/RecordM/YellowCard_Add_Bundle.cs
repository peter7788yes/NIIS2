﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class YellowCard_Add_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/YellowCard_Add_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.min.js"
                                                        ,"~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/Vaccination/RecordM/YellowCard_Add.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}