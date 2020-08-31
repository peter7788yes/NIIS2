using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class LeftMenu_Bundle
{
    public static void RegisterBundles(BundleCollection bundles, Predicate<string> predicate)
    {

        string cssPath = "~/bundles/LeftMenu_CSS";
        string jsPath = "~/bundles/LeftMenu_JS";

        List<string> cssList = new List<string>() {
                                                        "~/css/common.css"
                                                       ,"~/css/table.css"
                                                       ,"~/css/page.css"
                                                       ,"~/css/list.css"
                                                       ,"~/css/tree.css"
                                                  };


        List<string> jsList = new List<string>() {
                                                    "~/js/jq/jquery-2.1.4.js"
                                                    ,"~/LeftMenu.js"
                                                 };
        cssList = cssList.FindAll(predicate);
        jsList = jsList.FindAll(predicate);

        bundles.Add(new StyleBundle(cssPath).Include(cssList.ToArray()));
        bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));
    }




    
}