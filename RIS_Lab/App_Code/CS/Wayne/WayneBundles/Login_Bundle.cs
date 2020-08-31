using System;
using System.Collections.Generic;
using System.Web.Optimization;

public class Login_Bundle
{
		public static void RegisterBundles(BundleCollection bundles,Predicate<string> predicate)
        {
            string cssPath = "~/bundles/Login_CSS";
            string jsPath = "~/bundles/Login_JS";
            List<string> cssList = new List<string>() { "~/css/login.min.css" };
            List<string> jsList = new List<string>() { "~/Login.min.js" };

            cssList = cssList.FindAll(predicate);
            jsList = jsList.FindAll(predicate);

            bundles.Add(new StyleBundle(cssPath).Include(cssList.ToArray()));
            bundles.Add(new ScriptBundle(jsPath).Include(jsList.ToArray()));

        }
}