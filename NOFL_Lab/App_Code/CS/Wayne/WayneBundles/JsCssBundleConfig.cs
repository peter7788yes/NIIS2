using System;
using System.Collections.Generic;
using System.Web.Optimization;


namespace WayneBundles
{
    public class JsCssBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            string[] inMasterPageCss = new string[] { 
                                                           //"~/css/angular.treeview.css"
                                                           //"~/css/design.css",
                                                           //"~/css/common.css",
                                                           //"~/css/table.css",
                                                           //"~/css/page.css",
                                                           //"~/css/list.css"
                                                    };
           
            string[] inMasterPageJS = new string[]{
                                                   //"~/js/ang/angular-1.3.16.js"
                                                   };

            List<string> inMasterPageCssJS = new List<string>();
            inMasterPageCssJS.AddRange(inMasterPageCss);
            inMasterPageCssJS.AddRange(inMasterPageJS);

            bundles.Add(new ScriptBundle("~/bundles/MasterPage_JS").Include(inMasterPageJS));

            Predicate<string> predicate = ((item) => inMasterPageCssJS.Contains(item) == false);

           
            Common_Bundle.RegisterBundles(bundles, predicate);
            LeftMenu_Bundle.RegisterBundles(bundles, predicate);
            Login_Bundle.RegisterBundles(bundles, predicate);
            Date_Bundle.RegisterBundles(bundles, predicate);

            //AccountM
            AccountInfo_Bundle.RegisterBundles(bundles, predicate);
            AccountMaintain_Add_Bundle.RegisterBundles(bundles, predicate);
            AccountMaintain_Bundle.RegisterBundles(bundles, predicate);
            AccountMaintain_Detail_Bundle.RegisterBundles(bundles, predicate);

            //CertificateM
            ApplyData_Bundle.RegisterBundles(bundles, predicate);
            ApplyDataRecord_Bundle.RegisterBundles(bundles, predicate);
            PrintCertificate_Bundle.RegisterBundles(bundles, predicate);
            SignSetting_Bundle.RegisterBundles(bundles, predicate);

            //CodeM
            CodeSetting_Add_Bundle.RegisterBundles(bundles, predicate);
            CodeSetting_Bundle.RegisterBundles(bundles, predicate);
            CodeSetting_Detail_Bundle.RegisterBundles(bundles, predicate);


            //DocumentM
            DocumentMaintain_Add_Bundle.RegisterBundles(bundles, predicate);
            DocumentMaintain_Bundle.RegisterBundles(bundles, predicate);
            DocumentMaintain_Update_Bundle.RegisterBundles(bundles, predicate);


            //ParameterM
            BatchSetting_Bundle.RegisterBundles(bundles, predicate);
            LocationSetting_AddVaccine_Bundle.RegisterBundles(bundles, predicate);
            LocationSetting_Bundle.RegisterBundles(bundles, predicate);
            LocationSetting_SelectAgency_Bundle.RegisterBundles(bundles, predicate);
            LocationSetting_Update_Bundle.RegisterBundles(bundles, predicate);

            //PowerM
            RolePowerSetting_Add_Bundle.RegisterBundles(bundles, predicate);
            RolePowerSetting_AddPower_Bundle.RegisterBundles(bundles, predicate);
            RolePowerSetting_Bundle.RegisterBundles(bundles, predicate);
            RolePowerSetting_Update_Bundle.RegisterBundles(bundles, predicate);
            RolePowerSetting_UpdatePower_Bundle.RegisterBundles(bundles, predicate);

            //RecordM
            AddVaccine_Bundle.RegisterBundles(bundles, predicate);
            ApplyEffect_Bundle.RegisterBundles(bundles, predicate);
            ApplyHealth_Bundle.RegisterBundles(bundles, predicate);
            ApplyRecord_Bundle.RegisterBundles(bundles, predicate);
            ApplyRecord_ExpiredRecordReasone_Bundle.RegisterBundles(bundles, predicate);
            ApplyRecord_SelectAgency_Bundle.RegisterBundles(bundles, predicate);
            BCGRecord_Add_Bundle.RegisterBundles(bundles, predicate);
            BCGRecord_Bundle.RegisterBundles(bundles, predicate);
            BCGRecord_Detail_Bundle.RegisterBundles(bundles, predicate);
            CaseUserRemark_Add_Bundle.RegisterBundles(bundles, predicate);
            ChooseCate_Bundle.RegisterBundles(bundles, predicate);
            RecordDetail_Bundle.RegisterBundles(bundles, predicate);
            RegisterData_Bundle.RegisterBundles(bundles, predicate);
            RegisterData_Detail_Bundle.RegisterBundles(bundles, predicate);
            StudentRecord_Add_Bundle.RegisterBundles(bundles, predicate);
            StudentRecord_Bundle.RegisterBundles(bundles, predicate);
            StudentRecord_Detail_Bundle.RegisterBundles(bundles, predicate);
            StudentReRecord_Detail_Bundle.RegisterBundles(bundles, predicate);
            StudentReRecord_Add_Bundle.RegisterBundles(bundles, predicate);
            StudentReRecord_Bundle.RegisterBundles(bundles, predicate);
            StudentReRecord_Detail_Bundle.RegisterBundles(bundles, predicate);
            StudentReRecord_Upload_Bundle.RegisterBundles(bundles, predicate);
            YellowCard_Add_Bundle.RegisterBundles(bundles, predicate);


            //VaccinationM
            AbnormalVaccinationSchedule_Bundle.RegisterBundles(bundles, predicate);
            InoculationRecordTable_Bundle.RegisterBundles(bundles, predicate);
            OverdueVaccinationStatistics_Bundle.RegisterBundles(bundles, predicate);
            ReportTheNumberOfVaccineInoculation_Bundle.RegisterBundles(bundles, predicate);
            VaccinationSchedule_Bundle.RegisterBundles(bundles, predicate);
            VaccinationSchedule_SelectAgency_Bundle.RegisterBundles(bundles, predicate);
            VaccineOverdueEarlyVaccinationStatistics_Bundle.RegisterBundles(bundles, predicate);

        }




    }
}