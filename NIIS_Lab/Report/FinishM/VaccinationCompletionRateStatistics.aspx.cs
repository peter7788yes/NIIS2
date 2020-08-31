using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public partial class Report_FinishM_VaccinationCompletionRateStatistics : BasePage
{
    public string tbAry = "[]";
    public string DateStart = "";
    public string DateEnd = "";
    //private volatile bool success = false;

    public Report_FinishM_VaccinationCompletionRateStatistics()
    {
        base.AddPower("/Report/FinishM/VaccinationCompletionRateStatistics.aspx", MyPowerEnum.查詢);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
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

        UC_OpenSelectSingleOrg.PageUrl = "/Report/FinishM/VaccinationCompletionRateStatistics.aspx";

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
        var filePath = Path.Combine(System.IO.Path.GetDirectoryName(Request.PhysicalPath), Path.GetFileNameWithoutExtension(Request.PhysicalPath) + ".xlsx");
        Response.ContentType = "application/download";
        Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode("國小學童預防接種完成率統計") + Path.GetExtension(filePath));
        Response.TransmitFile(filePath);
        Response.End();
    }
}