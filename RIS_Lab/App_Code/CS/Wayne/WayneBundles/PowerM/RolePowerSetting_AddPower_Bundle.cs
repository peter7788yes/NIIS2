using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class RolePowerSetting_AddPower_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/RolePowerSetting_AddPower_JS";
            List<string> jsList = new List<string>(){
                                                         "~/js/sys/menuPath.min.js"
                                                        ,"~/bower_components/jquery/dist/jquery.min.js"
                                                        ,"~/System/PowerM/RolePowerSetting_AddPower.min.js"
                                                     };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}