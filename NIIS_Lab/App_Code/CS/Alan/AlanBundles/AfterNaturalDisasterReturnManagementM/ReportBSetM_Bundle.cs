using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

/// <summary>
/// ReportBSetM_Bundle 的摘要描述
/// </summary>
public class ReportBSetM_Bundle
{
    public static void RegisterBundles(BundleCollection bundles, Predicate<string> predicate)
    {
        string jsPath = "~/bundles/ReportBSet_JS";

        List<string> jsList = new List<string>(new[]{
                                    "~/js/jq/jquery-2.1.4.js"
                                  , "~/js/jq/url.js"
                                  , "~/js/sys/menuPath.min.js"
                                  , "~/js/ang/angular-1.3.16.js"
                                  , "~/js/ang/TableM.js"
                                  , "~/js/ang/PageM.min.js"
                                  , "~/Vaccine/AfterNaturalDisasterReturnManagementM/ReportBSet/ReportBSet.js"
                                 });

        jsList = jsList.FindAll(predicate);

        bundles.Add(new ScriptBundle(jsPath)
                        .Include(jsList.ToArray()));

    }
}