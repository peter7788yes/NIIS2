﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

/// <summary>
/// CommonBatchListM_Bundle 的摘要描述
/// </summary>
public class StockCommonPageM_Bundle
{
    public static void RegisterBundles(BundleCollection bundles, Predicate<string> predicate)
    {
        string jsPath = "~/bundles/StockCommonPage_JS";

        List<string> jsList = new List<string>(new[]{
                                    "~/js/jq/jquery-2.1.4.js"
                                  , "~/js/jq/url.js"
                                  , "~/js/sys/menuPath.min.js"
                                  , "~/js/ang/angular-1.3.16.js"
                                  , "~/js/ang/angular-sanitize-1.3.16.js"
                                  , "~/js/ang/FilterM.min.js"
                                  , "~/js/ang/TableM.js"
                                  , "~/js/ang/InputM.js"
                                  , "~/js/ang/PageM.min.js"
                                  , "~/Vaccine/StockManagementM/StockCommonPage/BatchList.js"
                                 });

        jsList = jsList.FindAll(predicate);

        bundles.Add(new ScriptBundle(jsPath)
                        .Include(jsList.ToArray()));
    }
}