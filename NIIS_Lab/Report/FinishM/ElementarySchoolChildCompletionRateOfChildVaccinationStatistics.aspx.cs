using System;
using System.IO;

public partial class Report_FinishM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics : BasePage
{
    public Report_FinishM_ElementarySchoolChildCompletionRateOfChildVaccinationStatistics()
    {
        base.AddPower("/Report/FinishM/ElementarySchoolChildCompletionRateOfChildVaccinationStatistics.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
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