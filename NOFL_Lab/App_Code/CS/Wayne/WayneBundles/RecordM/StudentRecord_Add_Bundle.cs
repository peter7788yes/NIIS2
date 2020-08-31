﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class StudentRecord_Add_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/StudentRecord_Add_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.js"
                                                        ,"~/js/jq/jquery-2.1.4.js"
                                                        ,"~/js/ang/angular-1.4.3.js"
                                                        ,"~/Vaccination/RecordM/StudentRecord_Add.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}