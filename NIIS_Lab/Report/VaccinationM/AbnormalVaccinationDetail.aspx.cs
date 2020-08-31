using System;
using System.IO;

public partial class Report_VaccinationM_AbnormalVaccinationDetail : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {

    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        var filePath = Path.Combine(System.IO.Path.GetDirectoryName(Request.PhysicalPath), Path.GetFileNameWithoutExtension(Request.PhysicalPath) + ".xlsx");
        Response.ContentType = "application/download";
        Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode("異常接種明細表") + Path.GetExtension(filePath));
        Response.TransmitFile(filePath);
        Response.End();
    }
}