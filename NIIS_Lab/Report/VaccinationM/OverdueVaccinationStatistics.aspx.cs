﻿using System;
using System.IO;
using System.Web;

public partial class Report_VaccinationM_OverdueVaccinationStatistics : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        int ReportType = 1;
        int.TryParse(Request.Form["rd"] ?? "1", out ReportType);
        var filePath = Path.Combine(System.IO.Path.GetDirectoryName(Request.PhysicalPath), Path.GetFileNameWithoutExtension(Request.PhysicalPath) + ReportType + ".xlsx");
        Response.ContentType = "application/download";
        string fileName = "逾期未接種統計表";
        if (ReportType > 1)
        {
            fileName = "逾期未接種明細表";
        }
        Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName) + Path.GetExtension(filePath));
        Response.TransmitFile(filePath);
        Response.End();
    }
}