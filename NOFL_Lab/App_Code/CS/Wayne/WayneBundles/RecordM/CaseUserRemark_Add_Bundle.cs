﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class CaseUserRemark_Add_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/CaseUserRemark_Add_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.js"
                                                        ,"~/js/jq/jquery-2.1.4.js"
                                                        ,"~/js/ang/angular-1.4.3.js"
                                                        ,"~/Vaccination/RecordM/CaseUserRemark_Add.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}