﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class ApplyData_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/ApplyData_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.min.js"
                                                        ,"~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/bower_components/angular/angular.min.js"
                                                        ,"~/js/ang/ToolM.min.js"
                                                        ,"~/Vaccination/CertificateM/ApplyData.min.js"
                                                     };

            jsList = jsList.FindAll(predicate);

            bundles.IgnoreList.Clear();
            //bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            bundles.IgnoreList.Ignore("~/js/date/calendar.js");
            bundles.IgnoreList.Ignore("~/js/date/WdatePicker.js");
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));
        }


        //public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        //{
        //    if (ignoreList == null)
        //        throw new ArgumentNullException("ignoreList");
        //    ignoreList.Ignore("*.intellisense.js");
        //    ignoreList.Ignore("*-vsdoc.js");
        //    ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        //    //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
        //    ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        //}

        //public static void RegisterBundles(BundleCollection bundles)
        //{
        //    bundles.IgnoreList.Clear();
        //    AddDefaultIgnorePatterns(bundles.IgnoreList);
        //    //NOTE: it's bundles.DirectoryFilter in Microsoft.AspNet.Web.Optimization.1.1.3 and not bundles.IgnoreList

        //    //...your code
        //}
}