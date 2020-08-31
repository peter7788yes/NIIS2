﻿using System;
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

          
			//VaccineInformationM
            DiseaseM_Bundle.RegisterBundles(bundles, predicate);
            VaccineM_Bundle.RegisterBundles(bundles, predicate);
			YCardMainM_Bundle.RegisterBundles(bundles, predicate);

            //VaccineParameterM
            OrgVaccSetM_Bundle.RegisterBundles(bundles, predicate);

			//StockManagementM
            StockCommonPageM_Bundle.RegisterBundles(bundles, predicate);
            VaccineCheckM_Bundle.RegisterBundles(bundles, predicate);
            VaccineInM_Bundle.RegisterBundles(bundles, predicate);
            VaccineOutM_Bundle.RegisterBundles(bundles, predicate);
            VaccineUseM_Bundle.RegisterBundles(bundles, predicate);
            VaccineDamM_Bundle.RegisterBundles(bundles, predicate);
            VaccineReturnM_Bundle.RegisterBundles(bundles, predicate);            
            StockQueryM_Bundle.RegisterBundles(bundles, predicate);
            KnotStockQueryM_Bundle.RegisterBundles(bundles, predicate);
            OrgsBalanceMonthlyReportM_Bundle.RegisterBundles(bundles, predicate);

            //AfterNaturalDisasterReturnManagementM
            ReportBMainM_Bundle.RegisterBundles(bundles, predicate);
            ReportBSetM_Bundle.RegisterBundles(bundles, predicate);

            //VaccineColdChainManagementM
            ColdChainM_Bundle.RegisterBundles(bundles, predicate);
            WarmSetM_Bundle.RegisterBundles(bundles, predicate);

            //VaccineM
            AverageOfMonthlyConsumesReportM_Bundle.RegisterBundles(bundles, predicate);
            ConsumedBalanceReportM_Bundle.RegisterBundles(bundles, predicate);
            DepletionRateReportM_Bundle.RegisterBundles(bundles, predicate);
            DamCasesArchiveReportsM_Bundle.RegisterBundles(bundles, predicate);
            InOutRecordsTableM_Bundle.RegisterBundles(bundles, predicate);
            NaturalDisasterTrackingReportM_Bundle.RegisterBundles(bundles, predicate);
            ReasonsDamStatisticalM_Bundle.RegisterBundles(bundles, predicate);

            //ElectronBulletinM
            NewsPublishedM_Bundle.RegisterBundles(bundles, predicate);
            MessageViewM_Bundle.RegisterBundles(bundles, predicate);

            //FrequentlyAskedQuestionM
            QnADataM_Bundle.RegisterBundles(bundles, predicate);
            QAViewM_Bundle.RegisterBundles(bundles, predicate);
            
            //SystemAlertM
            SystemAlertM_Bundle.RegisterBundles(bundles, predicate);

            //SystemSettingsM
            SendMessageAccountSetM_Bundle.RegisterBundles(bundles, predicate);
            SwitchAccountSetM_Bundle.RegisterBundles(bundles, predicate);
            SystemParametersM_Bundle.RegisterBundles(bundles, predicate);

        }




    }
}