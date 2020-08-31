using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

/// <summary>
/// StockQueryM_Bundle 的摘要描述
/// </summary>
public class StockQueryM_Bundle
{
    public static void RegisterBundles(BundleCollection bundles, Predicate<string> predicate)
    {
        string cssPath = "~/bundles/StockQuery_CSS";
        string jsPath = "~/bundles/StockQuery_JS";

        List<string> cssList = new List<string>() { 
                                              "~/css/design.css",
                                              "~/css/common.css",
                                              "~/css/table.css",
                                              "~/css/page.css",
                                              "~/css/list.css"
        };

        List<string> jsList = new List<string>(new[]{
                                    "~/js/jq/jquery-2.1.4.js"
                                  , "~/js/jq/url.js"
                                  , "~/js/sys/menuPath.js"
                                  , "~/js/ang/angular-1.3.16.js"
                                  , "~/js/ang/angular-sanitize-1.3.16.js"
                                  , "~/js/ang/FilterM.js"
                                  , "~/js/ang/TableM.js"
                                  , "~/js/ang/InputM.js"
                                  , "~/js/ang/PageM.js"
                                  , "~/Vaccine/StockManagementM/StockQuery/StockQuery.js"
                                 });

        cssList = cssList.FindAll(predicate);
        jsList = jsList.FindAll(predicate);

        bundles.Add(new StyleBundle(cssPath)
                        .Include(cssList.ToArray()));

        bundles.Add(new ScriptBundle(jsPath)
                        .Include(jsList.ToArray()));
    }
}