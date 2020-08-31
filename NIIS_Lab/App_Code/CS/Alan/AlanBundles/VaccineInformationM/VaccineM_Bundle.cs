using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// VaccineM_Bundle 的摘要描述
/// </summary>
public class VaccineM_Bundle
{
    public static void RegisterBundles(BundleCollection bundles, Predicate<string> predicate)
    {
        string jsPath = "~/bundles/Vaccine_JS";
                                                
        List<string> jsList = new List<string>(new[]{
                                    "~/js/jq/jquery-2.1.4.js"
                                  , "~/js/jq/url.js"
                                  , "~/js/sys/menuPath.min.js"
                                  , "~/js/ang/angular-1.3.16.js"
                                  , "~/js/ang/angular-cookies-1.3.16.js"
                                  , "~/js/ang/FilterM.min.js"
                                  , "~/js/ang/TableM.js"
                                  , "~/js/ang/PageM.min.js"
                                  , "~/Vaccine/VaccineInformationM/Vaccine/Vaccine.js"
                                  });

        jsList = jsList.FindAll(predicate);

        bundles.Add(new ScriptBundle(jsPath)
                        .Include(jsList.ToArray()));
    }
}