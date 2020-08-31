using System;
using System.Collections.Generic;
using System.IO;

public partial class Report_VaccinationM_VaccineOverdueEarlyVaccinationStatistics_Print : BasePage
{
    public string tbData = "[]";

    public Report_VaccinationM_VaccineOverdueEarlyVaccinationStatistics_Print()
    {
        base.AddPower("/Report/VaccinationM/VaccineOverdueEarlyVaccinationStatistics.aspx", MyPowerEnum.列印);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        ReportMachine RM = new ReportMachine();
        RM.Run(ApplyType.預覽列印, Path.GetFileNameWithoutExtension(Request.PhysicalPath).Split('_')[0]
            , "1111", 1,1, new Dictionary<string, object>()
            , (data) => { tbData = data; });
    }
}