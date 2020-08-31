using System;
using System.Collections.Generic;
using System.IO;

public partial class Report_WorkloadM_ElementarySchoolChildVaccinationWorkloadStatistics : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(true);

        UC_OpenSelectSingleOrg.PageUrl = "/Report/WorkloadM/ElementarySchoolChildVaccinationWorkloadStatistics.aspx";
        UC_OpenSelectSingleOrg.IsRequired = true;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        ReportMachine RM = new ReportMachine();
        RM.Run(ApplyType.匯出xls, Path.GetFileNameWithoutExtension(Request.PhysicalPath).Split('_')[0]
            , "1111", 1,1 ,new Dictionary<string, object>(),(data)=> {  });
    }
}