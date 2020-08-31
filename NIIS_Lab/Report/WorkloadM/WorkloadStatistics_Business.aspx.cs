using System;

public partial class Report_WorkloadM_WorkloadStatistics_Business : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UC_OpenSelectSingleOrg.PageUrl = "/Report/WorkloadM/WorkloadStatistics_Business.aspx";
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
       
    }
}