using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class AccountInfo_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/AccountInfo_JS";
            List<string> jsList = new List<string>(){
                                                    "~/js/sys/menuPath.js"
                                                    ,"~/js/jq/jquery-2.1.4.min.js"
                                                     ,"~/System/AccountM/AccountInfo.js"
                                                   };

            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));


           

    }
}