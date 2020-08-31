using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class LeftMenu_Bundle
{
    public static void RegisterBundles(BundleCollection bundles, Predicate<string> predicate)
    {

        string cssPath = "~/bundles/LeftMenu_CSS";
        string jsPath = "~/bundles/LeftMenu_JS";

        List<string> cssList = new List<string>() {
                                                        "~/css/common.min.css"
                                                       ,"~/css/table.min.css"
                                                       ,"~/css/page.min.css"
                                                       ,"~/css/list.min.css"
                                                       ,"~/css/tree.min.css"
                                                  };


        List<string> jsList = new List<string>() {
                                                    "~/bower_components/jquery/dist/jquery.min.js"
                                                    ,"~/LeftMenu.min.js"
                                                 };
        cssList = cssList.FindAll(predicate);
        jsList = jsList.FindAll(predicate);

        bundles.Add(new StyleBundle(cssPath).Include(cssList.ToArray()));
        bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));
    }




    
}