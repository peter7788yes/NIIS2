﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

/// <summary>
/// VaccineUseM_Bundle 的摘要描述
/// </summary>
public class VaccineUseM_Bundle
{
    public static void RegisterBundles(BundleCollection bundles, Predicate<string> predicate)
    {
        string jsPath = "~/bundles/VaccineUse_JS";

        List<string> jsList = new List<string>(new[]{
                                    "~/js/jq/jquery-2.1.4.js"
                                  , "~/js/jq/url.js"
                                  , "~/js/sys/menuPath.js"
                                  , "~/js/ang/angular-1.3.16.js"
                                  , "~/js/ang/hotkeys.js"
                                  , "~/js/ang/angular-cookies-1.3.16.js"
                                  , "~/js/ang/FilterM.js"
                                  , "~/js/ang/TableM.js"
                                  , "~/js/ang/InputM.js"
                                  , "~/js/ang/PageM.js"
                                  , "~/Vaccine/StockManagementM/VaccineUse/VaccineUse.js"
                                 });

        jsList = jsList.FindAll(predicate);

        bundles.Add(new ScriptBundle(jsPath)
                        .Include(jsList.ToArray()));

    }
}