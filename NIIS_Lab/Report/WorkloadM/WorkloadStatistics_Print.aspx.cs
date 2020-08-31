using System;
using System.Collections.Generic;
using System.IO;

public partial class Report_WorkloadM_WorkloadStatistics_Print : BasePage
{
    public string tbData = "[]";

    public Report_WorkloadM_WorkloadStatistics_Print()
    {
        base.AddPower("/Report/VaccinationM/WorkloadStatistics.aspx", MyPowerEnum.列印);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        ReportMachine RM = new ReportMachine();
        RM.Run(ApplyType.預覽列印, Path.GetFileNameWithoutExtension(Request.PhysicalPath).Split('_')[0]
            , "1111", 1, 1, new Dictionary<string, object>()
            , (data) => { tbData = data; });
    }
}