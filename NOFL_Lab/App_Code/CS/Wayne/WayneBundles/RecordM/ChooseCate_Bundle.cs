using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class ChooseCate_Bundle
{ 
        public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/ChooseCate_JS";
            List<string> jsList = new List<string>(){
                                                        "~/js/jq/jquery-2.1.4.js"
                                                        ,"~/Vaccination/RecordM/ChooseCate.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}