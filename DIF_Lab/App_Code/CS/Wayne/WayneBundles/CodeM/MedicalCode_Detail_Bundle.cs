﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class MedicalCode_Detail_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/MedicalCode_Detail_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.js"
                                                        ,"~/js/jq/jquery-2.1.4.min.js"
                                                        ,"~/js/ang/angular-1.4.8.min.js"
                                                        ,"~/js/ang/PageM.js"
                                                        ,"~/js/ang/FilterM.js"
                                                        ,"~/System/CodeM/MedicalCode_Detail.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}