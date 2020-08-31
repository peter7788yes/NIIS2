using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public partial class Report_VaccinationM_InoculationRecordTable : BasePage
{
    public string tbAry = "[]";
    public string DateStart = "";
    public string DateEnd = "";
    //private volatile bool success = false;

    protected new void Page_Load(object sender, EventArgs e)
    {
            base.AllowHttpMethod("GET","POST");
            base.DisableTop(true);

            DateStart = GetString("ds");
            DateEnd = GetString("de");

            DateTime DateStartDate = DateTime.Now;
            DateTime DateEndDate = DateTime.Now;

            DateTime.TryParseExact((DateStart ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
                                   "yyyyMMdd",
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out DateStartDate);
            DateTime.TryParseExact((DateEnd ?? DateTime.Now.ToShortTaiwanDate()).RepublicToAD(),
                                "yyyyMMdd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out DateEndDate);

            UC_OpenSelectSingleOrg.PageUrl = "/Report/VaccinationM/InoculationRecordTable.aspx";

            List<SystemRecordVaccineVM> list = SystemRecordVaccine.list;

            //VaccineCheckPendDataVM VM = new VaccineCheckPendDataVM();
            //VM.BatchID = "123213";
            //string source = JsonConvert.SerializeObject(VM);
            //string hash = "";
            //using (MD5 md5Hash = MD5.Create())
            //{
            //     hash = GetMd5Hash(md5Hash, source);
            //}

            if (list.Count > 0)
            {
                var rtn = list.OrderBy(item => item.SystemRecordVaccineCode);
                tbAry = JsonConvert.SerializeObject(rtn);
            }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ReportMachine RM = new ReportMachine();
        RM.Run(ApplyType.匯出xls,"","1111",1,1, new Dictionary<string,object>());
    }
   
   
}