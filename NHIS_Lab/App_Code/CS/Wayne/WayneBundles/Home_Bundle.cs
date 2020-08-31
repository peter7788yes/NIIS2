using System;
using System.Collections.Generic;
using System.Web.Optimization;

/// <summary>
/// MasterBundle 的摘要描述
/// </summary>
public class Home_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string jsPath = "~/bundles/Home_JS";
            List<string> jsList = new List<string>() { "~/Home.js" };

            jsList = jsList.FindAll(predicate);

            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}