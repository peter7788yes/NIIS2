using System;

public partial class Report_VaccinationM_OverdueVaccinationStatistics_Print : BasePage
{
    public Report_VaccinationM_OverdueVaccinationStatistics_Print()
    {
        base.AddPower("/Report/VaccinationM/OverdueVaccinationStatistics.aspx", MyPowerEnum.查詢);
    }

    public int ReportType = 1;


    protected new void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.Equals("POST"))
        {
            int.TryParse(Request.Form["rt"] ?? "1", out ReportType);
        }
        else
        {
            Response.Write("");
            Response.End();
        }

    }
}