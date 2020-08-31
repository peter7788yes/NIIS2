﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class VaccinationDetail_SelectAgency_Bundle
{
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/VaccinationDetail_SelectAgency_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.js"
                                                        ,"~/js/jq/jquery-2.1.4.min.js"
                                                        ,"~/js/ang/angular-1.4.8.min.js"
                                                        ,"~/js/ang/hotkeys.min.js"
                                                        ,"~/js/ang/PageM.js"
                                                        ,"~/Report/RecordM/VaccinationDetail_SelectAgency.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}