﻿using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class LocationSetting_SelectAgency_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/LocationSetting_SelectAgency_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/sys/menuPath.js"
                                                        ,"~/js/jq/jquery-2.1.4.js"
                                                        ,"~/js/ang/angular-1.4.3.js"
                                                        ,"~/js/ang/PageM.js"
                                                        ,"~/js/ang/hotkeys.js"
                                                        ,"~/Vaccination/ParameterM/LocationSetting_SelectAgency.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}