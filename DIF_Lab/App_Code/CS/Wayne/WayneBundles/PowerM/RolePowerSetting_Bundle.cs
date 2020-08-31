using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class RolePowerSetting_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/RolePowerSetting_JS";
            List<string> jsList = new List<string>(){
                                                    "~/js/sys/menuPath.min.js"
                                                    ,"~/bower_components/jquery/dist/jquery.min.js"
                                                    ,"~/bower_components/angular/angular.min.js"
                                                    ,"~/js/ang/hotkeys.min.js"
                                                    ,"~/js/ang/PageM.min.js"
                                                    ,"~/js/ang/ToolM.min.js"
                                                    ,"~/System/PowerM/RolePowerSetting.min.js"
                                                 };
            jsList = jsList.FindAll(predicate);
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}