using System;

public partial class Report_VaccinationM_VaccinationDetail_Print : BasePage
{
    public int ReportType = 1;

    public Report_VaccinationM_VaccinationDetail_Print()
    {
        base.AddPower("/Report/VaccinationM/VaccinationDetail.aspx", MyPowerEnum.查詢);
    }

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