using System;
using System.Collections.Generic;
using System.Web.Optimization;


namespace WayneBundles
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //string[] inMasterPageCss = new string[]{"~/css/angular-csp.css"};
            string[] inMasterPageCss = new string[] { 
                                
                                                           //"~/css/angular.treeview.css"
                                                           //"~/css/design.css",
                                                           //"~/css/common.css",
                                                           //"~/css/table.css",
                                                           //"~/css/page.css",
                                                           //"~/css/list.css"
                                                    };
            //string[] inMasterPageHeadJS = new string[] { 
            //                                            //"~/css/angular-ui-tree.css"
            //                                            //"~/css/angular.treeview.css"
            //                                        };
            string[] inMasterPageJS = new string[]{
                                                   //"~/js/ang/angular-1.3.16.js"
                                                   };
            List<string> inMasterPageCssJS = new List<string>();
            inMasterPageCssJS.AddRange(inMasterPageCss);
            //inMasterPageCssJS.AddRange(inMasterPageHeadJS);
            inMasterPageCssJS.AddRange(inMasterPageJS);

            bundles.Add(new StyleBundle("~/bundles/MasterPage_CSS")
                            .Include(inMasterPageCss));
            //bundles.Add(new ScriptBundle("~/bundles/MasterPage_HeadJS")
            //               .Include(inMasterPageHeadJS));
            bundles.Add(new ScriptBundle("~/bundles/MasterPage_JS")
                            .Include(inMasterPageJS));

            Predicate<string> predicate = ((item) => inMasterPageCssJS.Contains(item) == false);

            //AccountM
            AccountInfo_Bundle.RegisterBundles(bundles, predicate);
            AccountMaintain_Bundle.RegisterBundles(bundles, predicate);

            //CodeM
            CodeSetting_Bundle.RegisterBundles(bundles, predicate);
            CodeSetting_Detail_Bundle.RegisterBundles(bundles, predicate);
            CodeSetting_Add_Bundle.RegisterBundles(bundles, predicate);

            //VaccineParameterM
            OrgVaccSetM_Bundle.RegisterBundles(bundles, predicate);

			//StockManagementM
            StockCommonPageM_Bundle.RegisterBundles(bundles, predicate);
            VaccineInM_Bundle.RegisterBundles(bundles, predicate);
            VaccineOutM_Bundle.RegisterBundles(bundles, predicate);
            VaccineUseM_Bundle.RegisterBundles(bundles, predicate);           
            StockQueryM_Bundle.RegisterBundles(bundles, predicate);
            KnotStockQueryM_Bundle.RegisterBundles(bundles, predicate);

            //ElectronBulletinM
            MessageViewM_Bundle.RegisterBundles(bundles, predicate);

            //FrequentlyAskedQuestionM
            QAViewM_Bundle.RegisterBundles(bundles, predicate);
            
            //SystemAlertM
            SystemAlertM_Bundle.RegisterBundles(bundles, predicate);

        }




    }
}