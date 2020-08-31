using System;
using System.IO;

public partial class Report_VaccinationM_VaccineOverdueEarlyVaccinationStatistics : BasePage
{ 
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        var filePath = Path.Combine(System.IO.Path.GetDirectoryName(Request.PhysicalPath), Path.GetFileNameWithoutExtension(Request.PhysicalPath) + ".xlsx");
        Response.ContentType = "application/download";
        Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode("預防接種紀錄表") + Path.GetExtension(filePath));
        Response.TransmitFile(filePath);
        Response.End();
    }
}